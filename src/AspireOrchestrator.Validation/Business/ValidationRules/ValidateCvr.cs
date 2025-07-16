using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Validation.Interfaces;
using AspireOrchestrator.Validation.Models;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AspireOrchestrator.Validation.Business.ValidationRules
{
    public class ValidateCvr : IValidationRule
    {
        private Regex regex = new Regex(@"^[1-9][0-9]{7}");

        public ValidationError? Validate(ReceiptDetail receiptDetail, ConcurrentBag<ValidationError> existingErrors)
        {
            const string errorMessage = "Ugyldigt Cvr-nummer";
            if (string.IsNullOrWhiteSpace(receiptDetail.Cvr) || !regex.IsMatch(receiptDetail.Cvr) ||
                receiptDetail.Cvr.Length != 8)
            {
                return ValidationHelper.AddValidationError(receiptDetail, existingErrors, ErrorCode.InvalidCvr,
                    errorMessage);
            }
            else
            {
                ValidationHelper.ResolveValidationError(existingErrors, ErrorCode.InvalidCvr);
                return null; // No error found, return null
            }
        }
    }
}