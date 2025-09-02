using AspireOrchestrator.Core.OrchestratorModels;
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using System.Text.Json.Serialization;
using AspireOrchestrator.Messaging.Interfaces;

namespace AspireOrchestrator.Messaging.Business
{
    public abstract class EventPublisher(ServiceBusClient serviceBusClient): IEventPublisher
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        public async Task PublishEvent(EventEntity eventEntity, string topicName)
        {
            var sender = serviceBusClient.CreateSender(topicName);

            var messageJson = JsonSerializer.Serialize(eventEntity, _options);
            // create a message that we can send
            var sbMessage = new ServiceBusMessage(messageJson);

            // send the message
            await sender.SendMessageAsync(sbMessage);
        }
    }
}
