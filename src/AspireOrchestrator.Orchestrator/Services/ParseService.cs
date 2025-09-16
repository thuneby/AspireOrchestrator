using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.Services
{
    public class ParseService(HttpClient httpClient, ILogger<EventService> logger): ServiceInvocationBase(httpClient, logger)
    {
        public async Task<EventEntity> HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            return await HandleEventAsync(eventEntity, "/api/parse/parse", cancellationToken);
        }
    }
}
