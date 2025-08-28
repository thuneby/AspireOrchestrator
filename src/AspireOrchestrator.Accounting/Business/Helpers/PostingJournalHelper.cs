using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Accounting.Business.Helpers
{
    internal static class PostingJournalHelper
    {
        internal static PostingJournal CreatePostingJournal(DateTime postingDate, string postingPurpose)
        {
            return new PostingJournal
            {
                PostingDate = postingDate,
                PostingPurpose = postingPurpose
            };
        }

        internal static void SetForeignKeys(PostingJournal postingJournal)
        {
            foreach (var entry in postingJournal.PostingEntries)
            {
                entry.PostingJournalId = postingJournal.Id;
            }
        }

        internal static bool ValidateJournal(PostingJournal journal)
        {
            if (journal.PostingEntries.Count == 0)
                return false;
            var creditAmounts = journal.PostingEntries.Sum(x => x.CreditAmount);
            var debitAmounts = journal.PostingEntries.Sum(x => x.DebitAmount);
            var currencies = journal.PostingEntries.GroupBy(x => x.Currency);
            return (debitAmounts - creditAmounts == 0 && currencies.Count() == 1);
        }
    }
}
