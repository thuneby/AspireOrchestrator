using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Parsing.Interfaces
{
    public interface IAsyncParser
    {
        Task<IEnumerable<ReceiptDetail>> ParseAsync(Stream payload, DocumentType documentType);
    }
}
