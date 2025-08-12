using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.DataAccess
{
    public class FlowRepository(OrchestratorContext context, ILogger<GuidRepositoryBase<Flow>> logger) : GuidRepositoryBase<Flow>(context, logger), IFlowRepository
    {
        public new Flow? Get(Guid id)
        {
            return context.Flow
                .Include(x => x.Events.OrderBy(e => e.ProcessState))
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
