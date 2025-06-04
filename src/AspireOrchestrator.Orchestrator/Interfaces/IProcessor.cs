using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Orchestrator.Interfaces
{
    public interface IProcessor
    {
        Task<EventEntity> ProcessEvent(EventEntity entity);
    }
}
