using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.PaymentProcessing.Interfaces
{
    public interface IProcessPayment
    {
        Task<List<MatchResult>> MatchDocumentTypeAsync(DocumentType documentType); 
        Task<MatchResult> MatchDocumentAsync(Guid documentId); 

        Task<List<MatchResult>> MatchAllAsync();
    }
}
