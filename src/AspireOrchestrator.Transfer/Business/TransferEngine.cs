using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Transfer.Business.Helpers;
using AspireOrchestrator.Transfer.DataAccess;
using AspireOrchestrator.Transfer.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Transfer.Business
{
    public class TransferEngine(ReceiptDetailRepository receiptDetailRepository, PostingRepository postingRepository, TransferRepository transferRepository, ILoggerFactory loggerFactory)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<TransferEngine>();

        public async Task<int> TransferDocumentAsync(Guid documentId)
        {
            // Transfer logic here
            var candidates = await receiptDetailRepository.GetByDocumentIdAsync(documentId);
            var receiptDetails = candidates.Where(x =>
                x.ReconcileStatus == ReconcileStatus.Paid && x.TransferStatus == TransferStatus.New).ToList();
            if (receiptDetails.Count == 0)
                return 0;
            return await TransfersReceiptDetails(receiptDetails);
        }

        private async Task<int> TransfersReceiptDetails(List<ReceiptDetail> receiptDetails)
        {
            var postings = await postingRepository.GetReceiptDetailsPostingsEntries(receiptDetails.Select(x => x.Id).ToList());
            var transfers = new List<TransferBase>();
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
            }

            await TransferHelper.SendTransfers(transfers);
            receiptDetails.ForEach(x => x.TransferStatus = TransferStatus.Sent);
            receiptDetails.ForEach(x => x.ReconcileStatus = ReconcileStatus.Sent);
            // ToDo create journal to Post to sent account
            // ToDo add journal to context
            await transferRepository.AddRange(transfers);
            await receiptDetailRepository.UpdateRange(receiptDetails);
            return transfers.Count;
        }

        public async Task<int> TransferAllAsync()
        {
            var receiptDetails = await receiptDetailRepository.GetReceiptDetailsForTransfer();
            return 0;
        }

        public async Task<int> HandleReplies()
        {
            // ToDo
            return 0;
        }
    }
}
