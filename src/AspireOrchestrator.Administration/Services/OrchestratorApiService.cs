using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Administration.Services
{
    public class OrchestratorApiService(HttpClient httpClient, ILogger<OrchestratorApiService> logger)
    {
        public async Task HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Handling event: {EventId}", eventEntity.Id);

            // call orchestrator
            var response = await httpClient.PostAsJsonAsync("/api/event/SaveAndExecuteEvent", eventEntity, cancellationToken);

            logger.LogInformation("Event handled successfully." + response);
        }
    }
}
