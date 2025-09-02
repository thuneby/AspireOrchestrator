using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.Services
{
    public class TransferService(HttpClient httpClient, ILogger<EventService> logger) : ServiceInvocationBase(httpClient, logger)
    {
        public async Task<EventEntity> HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            return eventEntity.DocumentType switch
            {
                DocumentType.IpStandard => await HandleEventAsync(eventEntity, "/api/Transfer/TransferDocument", cancellationToken),
                DocumentType.IpUdvidet => await HandleEventAsync(eventEntity, "/api/Transfer/TransferDocument", cancellationToken),
                DocumentType.NetsIs => await HandleEventAsync(eventEntity, "/api/Transfer/TransferDocument", cancellationToken),
                DocumentType.Camt53 => await HandleEventAsync(eventEntity, "/api/Transfer/TransferAll", cancellationToken),
                DocumentType.Camt54 => await HandleEventAsync(eventEntity, "/api/Transfer/TransferAll", cancellationToken),
                DocumentType.PosteringsData => await HandleEventAsync(eventEntity, "/api/Transfer/TransferAll", cancellationToken),
                _ => await HandleEventAsync(eventEntity, "/api/Transfer/TransferAll", cancellationToken)
            };
        }
    }
}
