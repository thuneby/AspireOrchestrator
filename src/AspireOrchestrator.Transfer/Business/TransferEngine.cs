using AspireOrchestrator.Accounting.Business;
using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Transfer.Business.Helpers;
using AspireOrchestrator.Transfer.DataAccess;
using AspireOrchestrator.Transfer.Interfaces;
using AspireOrchestrator.Transfer.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Transfer.Business
{
    public class TransferEngine(ReceiptDetailRepository receiptDetailRepository, PostingRepository postingRepository, TransferRepository transferRepository, ILoggerFactory loggerFactory): ITransferEngine
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<TransferEngine>();
        private readonly TransferQueueManager _queueManager = new TransferQueueManager(loggerFactory);
        public string TransferQueue { get; set; } = "Transfers"; // ToDo move to configuration
        public string ReplyQueue { get; set; } = "Replies"; // ToDo move to configuration

        public async Task<int> TransferDocumentAsync(Guid documentId)
        {
            // Transfer logic here
            var candidates = await receiptDetailRepository.GetByDocumentIdAsync(documentId);
            var receiptDetails = candidates.Where(x =>
                x.ReconcileStatus == ReconcileStatus.Paid).ToList();
            if (receiptDetails.Count == 0)
                return 0;
            return await TransfersReceiptDetails(receiptDetails);
        }

        public async Task<int> TransferAllAsync()
        {
            var receiptDetails = (await receiptDetailRepository.GetReceiptDetailsForTransfer()).ToList();
            if (receiptDetails.Count == 0)
                return 0;
            return await TransfersReceiptDetails(receiptDetails);
        }

        public async Task<int> Resend(Guid id)
        {
            var receiptDetail = receiptDetailRepository.Get(id);
            if (receiptDetail == null || receiptDetail.ReconcileStatus == ReconcileStatus.Closed)
                return 0;
            if (receiptDetail.ReconcileStatus == ReconcileStatus.Paid)
            {
                var list = new List<ReceiptDetail> { receiptDetail };
                return await TransfersReceiptDetails(list);
            }

            var transfer = transferRepository.GetQueryList().SingleOrDefault(x => x.Parent == receiptDetail.Id);
            if (transfer == null)
                return 0;
            transfer.TransferCount++;
            var result = _queueManager.Put(transfer, TransferQueue);
            if (!result)
                return 0;
            transferRepository.Update(transfer);
            return 1;
        }


        public async Task<int> HandleReplies()
        {
            var queueLength = _queueManager.GetQueueLength(ReplyQueue);
            if (queueLength == 0)
                return 0;
            var replies = new List<BackendReply>();
            for (var i = 0; i < queueLength; i++)
            {
                var reply = _queueManager.Get(ReplyQueue);
                if (reply == null) continue;
                replies.Add(reply);
            }

            var successReplies = replies.Where(x => x.Success).ToList();
            var failedReplies = replies.Where(x => !x.Success).ToList();

            await HandleSuccess(successReplies);
            await HandleFailed(failedReplies);

            return replies.Count;
        }

        private async Task HandleFailed(List<BackendReply> replies)
        {
            var transferIds = replies.Select(x => x.TransferId).ToList();
            var transfers = transferRepository.GetQueryList().Where(x => transferIds.Contains(x.Id)).ToList();
            if (transfers.Count == 0)
                return;
            foreach (var transfer in transfers)
            {
                var reply = replies.First(x => x.TransferId == transfer.Id);
                transfer.TransferStatus = TransferStatus.Error;
                transfer.Reply = reply.Message;
            }
            await transferRepository.UpdateRange(transfers);
        }

        private async Task HandleSuccess(List<BackendReply> replies)
        {
            var transferIds = replies.Select(x => x.TransferId).ToList();
            var transfers = transferRepository.GetQueryList().Where(x => transferIds.Contains(x.Id)).ToList();
            if (transfers.Count == 0)
                return;
            var parentIds = transfers.Select(x => x.Parent).ToList();
            var parents = receiptDetailRepository.GetQueryList().Where(x => parentIds.Contains(x.Id)).ToList();
            var receiptDetails = new List<ReceiptDetail>();
            var accepted = new List<Guid>();
            foreach (var transfer in transfers)
            {
                if (transfer.TransferStatus == TransferStatus.Accepted)
                    continue; // Idempotent
                var receiptDetail = parents.FirstOrDefault(x => x.Id == transfer.Parent);
                if (receiptDetail == null)
                    continue;
                var reply = replies.FirstOrDefault(x => x.TransferId == transfer.Id);
                transfer.Reply = reply?.Message ?? string.Empty;
                transfer.TransferStatus = TransferStatus.Accepted;
                accepted.Add(transfer.Id);
                receiptDetail.ReconcileStatus = ReconcileStatus.Closed;
                receiptDetails.Add(receiptDetail);

            }
            // ToDo Finalize postings of money out
            var postings = postingRepository.GetQueryList().Where(x => x.DocumentId != null && accepted.Contains(x.DocumentId.Value)).ToList();
            var journal = PostingEngine.FinalizeTransfers(postings);
            await postingRepository.AddPostingJournal(journal);
            await receiptDetailRepository.UpdateRange(receiptDetails);
        }

        private async Task<int> TransfersReceiptDetails(List<ReceiptDetail> receiptDetails)
        {
            var postings = await postingRepository.GetReceiptDetailsPostingsEntries(receiptDetails.Select(x => x.Id).ToList());
            var transfers = new List<TransferBase>();
            var postingSet = new List<(PostingEntry, Guid)>();
            foreach (var receiptDetail in receiptDetails)
            {
                var posting = postings.FirstOrDefault(x => x.DocumentId == receiptDetail.Id);
                if (posting == null)
                {
                    _logger.LogWarning("No posting found for receipt detail {ReceiptDetailId}", receiptDetail.Id);
                    continue;
                }
                var transfer = TransferHelper.CreateTransfer(receiptDetail, posting);
                transfers.Add(transfer);
                postingSet.Add((posting, transfer.Id));
            }

            await SendAndUpdateStatus(receiptDetails, transfers, postingSet);
            return transfers.Count;
        }

        private async Task SendAndUpdateStatus(List<ReceiptDetail> receiptDetails, List<TransferBase> transfers, List<(PostingEntry, Guid)> postingSet)
        {
            await _queueManager.PutAll(transfers, TransferQueue);
            receiptDetails.ForEach(x => x.ReconcileStatus = ReconcileStatus.Sent);
            var journal = PostingEngine.PostTransfers(postingSet);
            await postingRepository.AddPostingJournal(journal);
            await transferRepository.AddRange(transfers);
            await receiptDetailRepository.UpdateRange(receiptDetails);
        }
    }
}
