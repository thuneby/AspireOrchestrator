using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Messaging.Interfaces
{
    public interface IQueueManager
    {
        public bool Put(CloudEvent<object> entity, string queueName);
        public CloudEvent<object> Get(string queueName);
        public int GetQueueLength(string queueName);
    }
}
