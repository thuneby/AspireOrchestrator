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

    }
}
