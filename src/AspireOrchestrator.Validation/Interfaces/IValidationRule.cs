using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Validation.Models;
using System.Collections.Concurrent;

namespace AspireOrchestrator.Validation.Interfaces
{
    public interface IValidationRule
    {
        public ValidationError? Validate(ReceiptDetail receiptDetail, ConcurrentBag<ValidationError> existingErrors);
    }
}
