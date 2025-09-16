using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Messaging.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishEvent(EventEntity eventEntity, string topicName);
    }
}
