using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Validation.Interfaces
{
    public interface IValidator
    {
        Task<(bool valid, List<string> validationErrors)> ValidateAsync(ReceiptDetail receiptDetail);

        Task<(bool valid, List<string> validationErrors)> ValidateManyAsync(List<ReceiptDetail> receiptDetails);
    }
}
