using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Parsing.Interfaces
{
    public interface IDepositParser
    {
        Task<IEnumerable<Deposit>> ParseAsync(Stream payload, DocumentType documentType);
    }
}
