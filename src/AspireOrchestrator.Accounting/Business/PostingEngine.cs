using AspireOrchestrator.Accounting.Business.Helpers;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Accounting.Business
{
    public static class PostingEngine
    {
        public static PostingJournal PostDeposit(Deposit deposit)
        {
            var journal = PostingJournalHelper.CreatePostingJournal(deposit.TrxDate, "Deposit received");
            journal.PostingEntries.Add(PostingEntryHelper.CreateDepositPosting(deposit));
            journal.PostingEntries.Add(PostingEntryHelper.CreateDepositOffsetPosting(deposit));
            PostingJournalHelper.SetForeignKeys(journal);
            var valid = PostingJournalHelper.ValidateJournal(journal);
            return valid? journal : throw new ArgumentException("CreditDebitError : DepositId " + deposit.Id);
        }

        public static PostingJournal PostDepositReceiptDetailMatch(MatchResult matchResult)
        {
            var trxDate = matchResult.Deposits.Max(x => x.TrxDate);
            var valDate = matchResult.Deposits.Max(x => x.ValDate);
            var currency = matchResult.Deposits.First().Currency;
            var journal = PostingJournalHelper.CreatePostingJournal(trxDate, "Deposit closed");
            foreach (var entry in matchResult.Deposits.Select(deposit => PostingEntryHelper.CreateDepositClosingPostings(deposit)))
            {
                journal.PostingEntries.Add(entry);
            }

            foreach (var entry in matchResult.ReceiptDetails.Select(receiptDetail => PostingEntryHelper.CreateReceiptDetailPosting(receiptDetail, trxDate, valDate, currency)))
            {
                journal.PostingEntries.Add(entry);
            }
            PostingJournalHelper.SetForeignKeys(journal);
            var valid = PostingJournalHelper.ValidateJournal(journal);
            return valid ? journal : throw new ArgumentException("CreditDebitError : Match reference " + matchResult.PaymentReference);
        }

        public static PostingJournal PostTransfers(List<(PostingEntry, Guid)> postingSets)
        {
            const string message = "Transfer sent";
            var postingEntries = postingSets.Select(p => p.Item1).ToList();
            var trxDate = postingEntries.Max(x => x.BankTrxDate);
            var valDate = postingEntries.Max(x => x.BankValDate);
            var currency = postingEntries.First().Currency;
            var journal = PostingJournalHelper.CreatePostingJournal(trxDate, message);
            var totalDebit = postingEntries.Sum(x => x.DebitAmount) - postingEntries.Sum(x => x.CreditAmount);
            foreach (var entry in postingSets.Select(postingSet => PostingEntryHelper.CreateReversePosting(postingSet.Item1, message, postingSet.Item2)))
            {
                journal.PostingEntries.Add(entry);
            }

            var offset = PostingEntryHelper.CreateTransferOffsetPosting(trxDate, valDate, currency, 0M, totalDebit, "Transfers Sent");
            journal.PostingEntries.Add(offset);
            PostingJournalHelper.SetForeignKeys(journal);
            var valid = PostingJournalHelper.ValidateJournal(journal);
            return valid ? journal : throw new ArgumentException("CreditDebitError : Transfer out");
        }

        public static PostingJournal FinalizeTransfers(List<PostingEntry> postingEntries)
        {
            var message = "Transfers accepted";
            var trxDate = postingEntries.Max(x => x.BankTrxDate);
            var valDate = postingEntries.Max(x => x.BankValDate);
            var currency = postingEntries.First().Currency;
            var journal = PostingJournalHelper.CreatePostingJournal(trxDate, message);
            var totalDebit = postingEntries.Sum(x => x.DebitAmount) - postingEntries.Sum(x => x.CreditAmount);
            var transferPosting = PostingEntryHelper.CreateTransferOffsetPosting(trxDate, valDate, currency, totalDebit, 0M, message);
            var offset = PostingEntryHelper.CreateFinalizeTransferPosting(trxDate, valDate, currency, 0M, totalDebit, message);
            journal.PostingEntries.Add(transferPosting);
            journal.PostingEntries.Add(offset);
            return journal;
        }
    }
}
