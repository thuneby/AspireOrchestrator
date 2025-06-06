using System.Net.Http.Json;
using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.MessagingWorker.Services
{
    public class EventService(HttpClient httpClient, ILogger<EventService> logger)
    {
        public async Task HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Handling event: {EventId}", eventEntity.Id);

            // call orchestrator
            //var test = await httpClient.GetAsync("/api/event/GetAll");
            var response = await httpClient.PostAsJsonAsync("/api/event/SaveAndExecuteEvent", eventEntity, cancellationToken);

            logger.LogInformation("Event handled successfully." + response);
        }
    }
}
