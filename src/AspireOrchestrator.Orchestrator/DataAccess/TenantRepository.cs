using AspireOrchestrator.Core.Models;
using AspireOrchestrator.DataAccess.Interfaces;
using AspireOrchestrator.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.DataAccess
{
    public class TenantRepository : ModelRepositoryBase<Tenant>, IRepository<Tenant, long>
    {
        public TenantRepository(OrchestratorContext context, ILogger<TenantRepository> logger) : base(context, logger)
        {
        }
    }
}
