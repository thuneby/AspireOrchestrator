using AspireOrchestrator.DataAccess.Interfaces;
using AspireOrchestrator.Validation.Models;

namespace AspireOrchestrator.Validation.Interfaces
{
    public interface IValidationErrorRepository: IGuidRepository<ValidationError>
    {
        List<ValidationError> GetByReceiptDetailId(Guid receiptDetailId);
        List<ValidationError> GetOpenErrors(long tenantId);
    }
}
