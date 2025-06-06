using System.Text.Json;
using System.Text.Json.Serialization;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.MessagingWorker.Services;
using Azure.Messaging.ServiceBus;

namespace AspireOrchestrator.MessagingWorker;

public sealed class Worker(
    ILogger<Worker> logger,
    ServiceBusClient client,
    EventService eventService): BackgroundService
{
    private const string TopicName = "events";
    private const string SubscriptionName = "eventsubscription";
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Azure Service Bus queue
            var processor = client.CreateProcessor(
                TopicName,
                new ServiceBusProcessorOptions());

            // local
            //var processor = client.CreateProcessor(
            //    TopicName,
            //    SubscriptionName,
            //    new ServiceBusProcessorOptions());

            // Add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // Add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // Start processing
            await processor.StartProcessingAsync();

            logger.LogInformation("""
                                  Wait for a minute and then press any key to end the processing
                                  """);

            Console.ReadKey();

            // Stop processing
            logger.LogInformation("""

                                  Stopping the receiver...
                                  """);

            await processor.StopProcessingAsync();

            logger.LogInformation("Stopped receiving messages");
        }
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();

        logger.LogInformation("Received: {Body} from subscription.", body);

        var message = JsonSerializer.Deserialize<EventEntity>(body, _options);

        if (message == null)
        {
            logger.LogError("Failed to deserialize message: {Body}", body);
            await args.CompleteMessageAsync(args.Message);
            return;
        }

        await eventService.HandleEventAsync(message);

        // Complete the message. messages is deleted from the subscription.
        await args.CompleteMessageAsync(args.Message);
    }

    // Handle any errors when receiving messages
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        logger.LogError(args.Exception, "{Error}", args.Exception.Message);

        return Task.CompletedTask;
    }
}