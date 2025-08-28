using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Transfer.Models;

namespace AspireOrchestrator.Transfer.Business.Helpers
{
    public static class TransferHelper
    {
        public static TransferBase CreateTransfer(ReceiptDetail receiptDetail, PostingEntry posting)
        {
            return new TransferBase
            {
                BankTrxDate = posting.BankTrxDate,
                BankValDate = posting.BankValDate,
                Amount = posting.DebitAmount > 0 ? posting.DebitAmount : - posting.CreditAmount,
                PolicyNumber = receiptDetail.PolicyNumber?.ToString() ?? receiptDetail.Cpr,
                FromDate = receiptDetail.FromDate,
                ToDate = receiptDetail.ToDate,
                PaymentReference = receiptDetail.PaymentReference,
                TransferStatus = TransferStatus.Sent
            };
        }

        public static async Task SendTransfers(List<TransferBase> transfers)
        {
            throw new NotImplementedException();
        }
    }
}
