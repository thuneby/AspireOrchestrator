using Azure.Messaging.ServiceBus;
using AspireOrchestrator.Messaging.Business;

namespace AspireOrchestrator.Administration.Services
{
    public class EventPublisherService(ServiceBusClient serviceBusClient) : EventPublisher(serviceBusClient);
}
