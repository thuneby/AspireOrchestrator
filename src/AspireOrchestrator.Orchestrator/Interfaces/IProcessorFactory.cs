using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Orchestrator.Interfaces
{
    public interface IProcessorFactory
    {
        public IProcessor? GetProcessor(EventEntity entity);
    }
}
