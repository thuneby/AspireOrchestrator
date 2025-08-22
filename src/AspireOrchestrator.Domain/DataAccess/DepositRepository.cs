using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Domain.DataAccess
{
    public class DepositRepository(DomainContext context, ILogger<GuidRepositoryBase<Deposit>> logger) : GuidRepositoryBase<Deposit>(context, logger)
    {
    }
}
