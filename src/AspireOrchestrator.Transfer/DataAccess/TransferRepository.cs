using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Transfer.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Transfer.DataAccess
{
    public class TransferRepository(TransferContext context, ILogger<GuidRepositoryBase<TransferBase>> logger) : GuidRepositoryBase<TransferBase>(context, logger)
    {
    }
}
