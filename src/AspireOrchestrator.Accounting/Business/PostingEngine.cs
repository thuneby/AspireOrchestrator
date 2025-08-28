using AspireOrchestrator.Accounting.Business.Helpers;
using AspireOrchestrator.Accounting.Models;
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
            var valid = PostingJournalHelper.ValidateJournal(journal);
            return valid? journal : throw new ArgumentException("CreditDebitError : DepositId " + deposit.Id);
        }

        public PostingJournal PostDepositReceiptDetailMatch(MatchResult matchResult)
        {
            var trxDate = matchResult.Deposits.Max(x => x.TrxDate);
            var journal = PostingJournalHelper.CreatePostingJournal(trxDate, "Deposit received");
            foreach (var deposit in matchResult.Deposits)
            {
                journal.PostingEntries.Add(PostingEntryHelper.CreateDepositClosingPostings(deposit));
            }

            foreach (var receiptDetail in matchResult.ReceiptDetails)
            {
                journal.PostingEntries.Add(PostingEntryHelper.CreateReceiptDetailPosting(receiptDetail));   
            }
            var valid = PostingJournalHelper.ValidateJournal(journal);
            return valid ? journal : throw new ArgumentException("CreditDebitError : Match reference " + matchResult.PaymentReference);
        }

    }
}
