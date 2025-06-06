using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.DataAccess.Interfaces;

namespace AspireOrchestrator.Orchestrator.Interfaces
{
    public interface IFlowRepository: IRepository<Flow, long>
    {
    }
}
