using AspireOrchestrator.Messaging.Business;
using Azure.Messaging.ServiceBus;

namespace AspireOrchestrator.MessagingWorker.Services
{
    public class EventPublisherService(ServiceBusClient serviceBusClient) : EventPublisher(serviceBusClient)
    {
    }
}
