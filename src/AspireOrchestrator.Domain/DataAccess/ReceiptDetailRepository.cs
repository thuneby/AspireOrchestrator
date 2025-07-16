using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Domain.DataAccess
{
    public class ReceiptDetailRepository(DomainContext context, ILogger<GuidRepositoryBase<ReceiptDetail>> logger) : GuidRepositoryBase<ReceiptDetail>(context, logger)
    {
    }
}
