using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.Services
{
    public class ValidateService(HttpClient httpClient, ILogger<EventService> logger) : ServiceInvocationBase(httpClient, logger)
    {
        public async Task<EventEntity> HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            return await HandleEventAsync(eventEntity, "/api/Validation/ValidateDocument", cancellationToken);
        }
    }
}
