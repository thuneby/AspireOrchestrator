using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Validation.Models;

namespace AspireOrchestrator.Validation.Interfaces
{
    public interface IValidator
    {
        Task<ValidationResult> ValidateAsync(ReceiptDetail receiptDetail);

        Task<List<ValidationResult>> ValidateManyAsync(List<ReceiptDetail> receiptDetails);
    }
}
