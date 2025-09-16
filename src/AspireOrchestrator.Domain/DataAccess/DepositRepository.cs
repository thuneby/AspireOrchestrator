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

        public async Task<IEnumerable<Deposit>> GetByAccountNumberAsync(string accountNumber)
        {
            return await context.Deposit
                .Where(d => d.AccountNumber == accountNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Deposit>> GetByReconcileStatusAsync(ReconcileStatus reconcileStatus)
        {
            return await context.Deposit
                .Where(d => d.ReconcileStatus == reconcileStatus)
                .ToListAsync();
        }
    }
}
