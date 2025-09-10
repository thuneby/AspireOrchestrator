using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Domain.DataAccess
{
    public class PostingRepository(DomainContext context, ILogger<GuidRepositoryBase<PostingEntry>> logger) : GuidRepositoryBase<PostingEntry>(context, logger)
    {
        public async Task AddPostingJournal(PostingJournal journal)
        {
            context.PostingJournal.Add(journal);
            await context.AddRangeAsync(journal.PostingEntries);
        }

        public async Task<List<PostingEntry>> GetReceiptDetailPostingsEntries(Guid receiptDetailId)
        {
            return await Query().Where(x =>
                x.PostingDocumentType == DocumentType.ReceiptDetail && x.DocumentId == receiptDetailId).ToListAsync();
        }

        public async Task<List<PostingEntry>> GetReceiptDetailsPostingsEntries(List<Guid> ids)
        {
            return await Query().Where(x =>
                x.PostingDocumentType == DocumentType.ReceiptDetail && ids.Contains(x.DocumentId.Value)).ToListAsync();
        }

        public IQueryable<PostingSummary> GetPostingSummary()
        {
            var query = Query()
                .GroupBy(x => new { x.AccountType, x.Currency })
                .Select(g => new PostingSummary
                {
                    AccountType = g.Key.AccountType,
                    Currency = g.Key.Currency,
                    DebitBalance = g.Sum(x => x.DebitAmount),
                    CreditBalance = g.Sum(x => x.CreditAmount),
                    TotalBalance = g.Sum(x => x.DebitAmount - x.CreditAmount)
                });
            return query;
        }

        public IQueryable<PostingAccountSummary> GetPostingAccountSummary(DateTime balanceDate)
        {
            var query = Query()
                .Where(x => x.PostingJournal.PostingDate <= balanceDate)
                .GroupBy(x => new { x.AccountType, x.Currency, x.PostingAccount })
                .Select(g => new PostingAccountSummary
                {
                    AccountType = g.Key.AccountType,
                    Currency = g.Key.Currency,
                    PostingAccount = g.Key.PostingAccount,
                    DebitBalance = g.Sum(x => x.DebitAmount),
                    CreditBalance = g.Sum(x => x.CreditAmount),
                    TotalBalance = g.Sum(x => x.DebitAmount - x.CreditAmount),
                    BalanceDate = balanceDate
                });
            return query;
        }

        public IQueryable<PostingEntry> GetPostingEntries(AccountType type, string postingAccount) 
        {
            var query = context.PostingEntry.Include(x => x.PostingJournal)
                .Where(x => x.AccountType == type && x.PostingAccount == postingAccount);
            return query;
        }
    }
}
