using AspireOrchestrator.Accounting.Business.Helpers;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Accounting.Business
{
    public class PostingEngine
    {
        public PostingJournal PostDeposit(Deposit deposit)
        {
            var journal = PostingJournalHelper.CreatePostingJournal(deposit.TrxDate, "Deposit received");
            journal.PostingEntries.Add(PostingEntryHelper.CreateDepositPosting(deposit));
            journal.PostingEntries.Add(PostingEntryHelper.CreateDepositOffsetPosting(deposit));
            PostingJournalHelper.SetForeignKeys(journal);
            var valid = PostingJournalHelper.ValidateJournal(journal);
            return valid? journal : throw new ArgumentException("CreditDebitError : DepositId " + deposit.Id);
        }

        public PostingJournal PostDepositReceiptDetailMatch(MatchResult matchResult)
        {
            var trxDate = matchResult.Deposits.Max(x => x.TrxDate);
            var valDate = matchResult.Deposits.Max(x => x.ValDate);
            var currency = matchResult.Deposits.First().Currency;
            var journal = PostingJournalHelper.CreatePostingJournal(trxDate, "Deposit received");
            foreach (var deposit in matchResult.Deposits)
            {
                var entry = PostingEntryHelper.CreateDepositClosingPostings(deposit);
                journal.PostingEntries.Add(entry);
            }

            foreach (var receiptDetail in matchResult.ReceiptDetails)
            {
                var entry = PostingEntryHelper.CreateReceiptDetailPosting(receiptDetail, trxDate, valDate, currency);
                journal.PostingEntries.Add(entry);   
            }
            PostingJournalHelper.SetForeignKeys(journal);
            var valid = PostingJournalHelper.ValidateJournal(journal);
            return valid ? journal : throw new ArgumentException("CreditDebitError : Match reference " + matchResult.PaymentReference);
        }

    }
}
