using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Accounting.Business.Helpers
{
    internal static class PostingEntryHelper
    {
        internal static PostingEntry CreateDepositPosting(Deposit deposit)
        {
            return CreatePostingEntry(deposit.AccountNumber, AccountType.BankAccount, deposit.TrxDate, deposit.ValDate,
                0M, deposit.Amount, DocumentType.Deposit, deposit.Currency, deposit.PaymentReference, deposit.Id);
        }

        internal static PostingEntry CreateDepositOffsetPosting(Deposit deposit) 
        {
            return CreatePostingEntry("Offset", AccountType.OffsetAccount, deposit.TrxDate, deposit.ValDate,
                deposit.Amount, 0M, DocumentType.Deposit, deposit.Currency, deposit.PaymentReference, deposit.Id);
        }

        private static PostingEntry CreatePostingEntry(string account, AccountType accountType, DateTime trxDate, DateTime valDate, 
            decimal credit, decimal debit, DocumentType documentType, string currency, string message, Guid documentId)
        {
            var entry = new PostingEntry
            {
                PostingAccount = account,
                AccountType = accountType,
                BankTrxDate = trxDate,
                BankValDate = valDate,
                CreditAmount = credit > 0 ? credit: debit,
                DebitAmount = debit > 0 ? debit : credit,
                PostingDocumentType = documentType,
                Currency = currency,
                PostingMessage = message,
                DocumentId = documentId
            };
            return entry;
        }

        public static PostingEntry CreateDepositClosingPostings(Deposit deposit)
        {
            return CreatePostingEntry(deposit.AccountNumber, AccountType.BankAccount, deposit.TrxDate, deposit.ValDate,
                deposit.Amount, 0M, DocumentType.Deposit, deposit.Currency, "Closing " + deposit.PaymentReference, deposit.Id);
        }

        public static PostingEntry CreateReceiptDetailPosting(ReceiptDetail receiptDetail, DateTime trxDate, DateTime valDate, string currency)
        {
            var accountNumber = GetPersonPostingAccount(receiptDetail);
            return CreatePostingEntry(accountNumber, AccountType.Person, trxDate, valDate, 0M, receiptDetail.Amount, DocumentType.ReceiptDetail, currency, receiptDetail.PaymentReference, receiptDetail.Id);
        }

        private static string GetPersonPostingAccount(ReceiptDetail receiptDetail)
        {
            if (receiptDetail.PolicyNumber is > 0)
            {
                return receiptDetail.PolicyNumber.ToString();
            }

            return receiptDetail.PersonId > 0 ? receiptDetail.PersonId.ToString() : receiptDetail.Cpr;
        }
    }
}
