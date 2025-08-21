using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Domain.Interfaces
{
    public interface ITransactionDocument
    {
        ReconcileStatus ReconcileStatus { get; set; }
        Guid DocumentId { get; set; }
        long TenantId { get; set; }
    }
}
