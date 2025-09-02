using AspireOrchestrator.Core.OrchestratorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspireOrchestrator.Messaging.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishEvent(EventEntity eventEntity, string topicName);
    }
}
