using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.PaymentProcessing.Interfaces;
using AspireOrchestrator.PaymentProcessing.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.PaymentProcessing.Business
{
    public class PaymentProcessor(DomainContext context, ILoggerFactory loggerFactory): IProcessPayment
    {
        private readonly ReceiptDetailRepository _receiptDetailRepository = new ReceiptDetailRepository(context, loggerFactory.CreateLogger<ReceiptDetailRepository>());
        private readonly DepositRepository _depositRepository = new DepositRepository(context, loggerFactory.CreateLogger<DepositRepository>());

        public async Task<List<MatchResult>> MatchPaymentAsync(DocumentType documentType)
        {
            var receiptDetails = _receiptDetailRepository.GetQueryList()
                .Where(x => x.ReconcileStatus == ReconcileStatus.Open && x.DocumentType == documentType).ToList();
            var deposits = (await _depositRepository.GetByReconcileStatusAsync(ReconcileStatus.Paid)).ToList();
            if (receiptDetails.Count == 0 || deposits.Count == 0)
                return [];
            return await MatchAndReconcile(deposits, receiptDetails);
        }

        public async Task<MatchResult> MatchDocumentAsync(Guid documentId)
        {
            var receiptDetails = (await _receiptDetailRepository.GetByDocumentIdAsync(documentId)).ToList();
            var deposits = (await _depositRepository.GetByReconcileStatusAsync(ReconcileStatus.Paid)).ToList();
            if (receiptDetails.Count == 0 || deposits.Count == 0)
                return new MatchResult();
            var result= await MatchAndReconcile(deposits, receiptDetails);
            return result.FirstOrDefault() ?? new MatchResult();
        }

        public async Task<List<MatchResult>> MatchAllAsync()
        {
            var receiptDetails = _receiptDetailRepository.GetQueryList()
                .Where(x => x.ReconcileStatus == ReconcileStatus.Open).ToList();
            var deposits = (await _depositRepository.GetByReconcileStatusAsync(ReconcileStatus.Paid)).ToList();
            if (receiptDetails.Count == 0 || deposits.Count == 0)
                return [];
            return await MatchAndReconcile(deposits, receiptDetails);
        }


        private async Task<List<MatchResult>> MatchAndReconcile(List<Deposit> deposits, List<ReceiptDetail> receiptDetails)
        {
            var matchResult = GetMatchList(deposits, receiptDetails);
            if (matchResult.Count == 0)
                return [];
            await ReconcileMatchResultList(matchResult);
            return matchResult;
        }

        private async Task ReconcileMatchResultList(List<MatchResult> matchResult)
        {
            var receiptDetails = matchResult.SelectMany(m => m.ReceiptDetails).ToList();
            var deposits = matchResult.SelectMany(m => m.Deposits).ToList();
            foreach (var receiptDetail in receiptDetails)
            {
                receiptDetail.ReconcileStatus = ReconcileStatus.Paid;
            }
            foreach (var deposit in deposits)
            {
                deposit.ReconcileStatus = ReconcileStatus.Closed;
            }

            await using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await _receiptDetailRepository.UpdateRange(receiptDetails);
                await _depositRepository.UpdateRange(deposits);
                await transaction.CommitAsync();
            }
        }

        private static List<MatchResult> GetMatchList(List<Deposit> deposits, List<ReceiptDetail> receiptDetails)
        {
            var depositReferences = deposits.Select(x => x.PaymentReference.ToUpperInvariant());
            var receiptReferences = receiptDetails.Select(x => x.PaymentReference.ToUpperInvariant());

            var matches = depositReferences.Where(r => receiptReferences.Contains(r)).ToList();
            if (matches.Count == 0)
                return [];

            var depositList = deposits.GroupBy(d => d.PaymentReference.ToUpperInvariant())
                .Select(group => new
                {
                    group.Key,
                    TotalAmount = group.Sum(x => x.Amount)

                });

            var receiptList = receiptDetails.GroupBy(r => r.PaymentReference.ToUpperInvariant())
                .Select(group => new
                {
                    group.Key,
                    TotalAmount = group.Sum(x => x.Amount)

                });

            var referenceMatches = depositList.Where(x => receiptList.Contains(x)).Select(y => y.Key).ToList();

            return referenceMatches.Select(reference => 
                new MatchResult
                {
                    PaymentReference = reference, 
                    Deposits = deposits.Where(d => d.PaymentReference.Equals(reference, StringComparison.OrdinalIgnoreCase)).ToList(), 
                    ReceiptDetails = receiptDetails.Where(r => r.PaymentReference.Equals(reference, StringComparison.OrdinalIgnoreCase)).ToList()
                }).ToList();
        }
    }
}
