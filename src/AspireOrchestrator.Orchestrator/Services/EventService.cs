using System.Net.Http.Json;
using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.Services
{
    public class EventService(HttpClient httpClient, ILogger<EventService> logger)
    {
        public async Task HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Handling event: {EventId}", eventEntity.Id);

            // for debugging purposes, increase timeout from default 30 seconds to 5 minutes
            httpClient.Timeout = TimeSpan.FromMinutes(5);

            // call orchestrator
            //var test = await httpClient.GetAsync("/api/event/GetAll");
            var response = await httpClient.PostAsJsonAsync("/api/event/SaveAndExecuteEvent", eventEntity, cancellationToken);

            logger.LogInformation("Event handled successfully." + response);
        }
    }
}
