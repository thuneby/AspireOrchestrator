using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.PaymentProcessing.Models;

namespace AspireOrchestrator.PaymentProcessing.Interfaces
{
    public interface IProcessPayment
    {
        Task<List<MatchResult>> MatchPaymentAsync(DocumentType documentType);
        Task<MatchResult> MatchDocumentAsync(Guid documentId); 

        Task<List<MatchResult>> MatchAllAsync();
    }
}
