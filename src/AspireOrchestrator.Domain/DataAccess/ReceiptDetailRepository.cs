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
            return await Query().Where(x => x.DocumentId == documentId).ToListAsync();
        }


        public async Task<IEnumerable<ReceiptDetail>> GetReceiptDetailsForTransfer()
        {
            return await GetByReconcileStatusAsync(ReconcileStatus.Paid).ToArrayAsync();
        }

        private IQueryable<ReceiptDetail> GetByReconcileStatusAsync(ReconcileStatus reconcileStatus)
        {
            return Query().Where(x => x.ReconcileStatus == reconcileStatus);
        }

    }
}
