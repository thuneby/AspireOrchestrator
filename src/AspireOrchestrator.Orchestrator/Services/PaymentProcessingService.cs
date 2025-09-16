using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.Services
{
    public class PaymentProcessingService(HttpClient httpClient, ILogger<EventService> logger) : ServiceInvocationBase(httpClient, logger)
    {
        public async Task<EventEntity> HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            return eventEntity.DocumentType switch
            {
                DocumentType.IpStandard => await HandleEventAsync(eventEntity, "/api/PaymentProcessing/MatchDocument",
                    cancellationToken),
                DocumentType.IpUdvidet => await HandleEventAsync(eventEntity, "/api/PaymentProcessing/MatchDocument",
                    cancellationToken),
                DocumentType.NetsIs => await HandleEventAsync(eventEntity, "/api/PaymentProcessing/MatchDocumentType",
                    cancellationToken),
                DocumentType.Camt53 => await HandleEventAsync(eventEntity, "/api/PaymentProcessing/MatchAll",
                    cancellationToken),
                DocumentType.Camt54 => await HandleEventAsync(eventEntity, "/api/PaymentProcessing/MatchAll",
                    cancellationToken),
                DocumentType.PosteringsData => await HandleEventAsync(eventEntity, "/api/PaymentProcessing/MatchAll",
                    cancellationToken),
                _ => throw new NotSupportedException(
                    $"Document type {eventEntity.DocumentType} is not supported for payment processing")
            };
        }
    }
}
