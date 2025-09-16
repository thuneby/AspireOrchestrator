using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace AspireOrchestrator.Orchestrator.Services
{
    public class ServiceInvocationBase(HttpClient httpClient, ILogger<EventService> logger)
    {
        public async Task<EventEntity> HandleEventAsync(EventEntity eventEntity, string requestUri, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Handling event: {EventId}", eventEntity.Id);

            // for debugging purposes, increase timeout from default 30 seconds to 5 minutes
            httpClient.Timeout = TimeSpan.FromMinutes(5);

            var response = await httpClient.PostAsJsonAsync(requestUri, eventEntity, cancellationToken);

            logger.LogInformation("Request: " + response.RequestMessage);
            logger.LogInformation("Response code: " + response.StatusCode);
            logger.LogInformation("response reason: " + response.ReasonPhrase?? "");

            var result = await response.Content.ReadFromJsonAsync<EventEntity>(cancellationToken: cancellationToken);
            return result;
        }
    }
}
