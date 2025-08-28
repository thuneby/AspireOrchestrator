using AspireOrchestrator.Accounting.Models;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Accounting.Business.Helpers
{
    internal static class PostingEntryHelper
    {
        internal static PostingEntry CreateDepositPosting(Deposit deposit)
        {
            return CreatePostingEntry(deposit.AccountNumber, AccountType.BankAccount, deposit.TrxDate, deposit.ValDate,
                0M, deposit.Amount, "Deposit", deposit.Currency, deposit.PaymentReference, deposit.Id);
        }

        internal static PostingEntry CreateDepositOffsetPosting(Deposit deposit) 
        {
            return CreatePostingEntry("Offset", AccountType.OffsetAccount, deposit.TrxDate, deposit.ValDate,
                deposit.Amount, 0M, "Deposit", deposit.Currency, deposit.PaymentReference, deposit.Id);
        }

        private static PostingEntry CreatePostingEntry(string account, AccountType accountType, DateTime trxDate, DateTime valDate, 
            decimal credit, decimal debit, string documentType, string currency, string message, Guid documentId)
        {
            var entry = new PostingEntry
            {
                PostingAccount = account,
                AccountType = accountType,
                BankTrxDate = trxDate,
                BankValDate = valDate,
                CreditAmount = credit,
                DebitAmount = debit,
                PostingDocumentType = documentType,
                Currency = currency,
                PostingMessage = message,
                DocumentId = documentId
            };
            return entry;
        }

        public static PostingEntry CreateDepositClosingPostings(Deposit deposit)
        {
            throw new NotImplementedException();
        }

        public static PostingEntry CreateReceiptDetailPosting(ReceiptDetail receiptDetail)
        {
            throw new NotImplementedException();
        }
    }
}
