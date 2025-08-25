using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Domain.DataAccess
{
    public class DepositRepository(DomainContext context, ILogger<GuidRepositoryBase<Deposit>> logger) : GuidRepositoryBase<Deposit>(context, logger)
    {
        public async Task<IEnumerable<Deposit>> GetByDocumentIdAsync(Guid documentId)
        {
            return await context.Deposit
                .Where(rd => rd.DocumentId == documentId)
                .ToListAsync();
        }
    }
}
