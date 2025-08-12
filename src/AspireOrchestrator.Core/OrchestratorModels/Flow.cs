using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Core.OrchestratorModels
{
    public class Flow : GuidModelBase
    {
        public FlowState State { get; set; }
        public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }
}
