using AspireOrchestrator.Messaging.Business;
using Azure.Messaging.ServiceBus;

namespace AspireOrchestrator.Orchestrator.WebApi.Services
{
    public class EventPublisherService(ServiceBusClient serviceBusClient) : EventPublisher(serviceBusClient);
}
