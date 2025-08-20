using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Domain.DataAccess
{
    public class ReceiptDetailRepository(DomainContext context, ILogger<GuidRepositoryBase<ReceiptDetail>> logger) : GuidRepositoryBase<ReceiptDetail>(context, logger)
    {
        public async Task<IEnumerable<ReceiptDetail>> GetByDocumentIdAsync(Guid documentId)
        {
            return await context.ReceiptDetail
                .Where(rd => rd.DocumentId == documentId)
                .ToListAsync();
        }
    }
}
