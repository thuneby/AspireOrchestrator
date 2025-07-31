using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Validation.Interfaces;
using AspireOrchestrator.Validation.Models;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AspireOrchestrator.Validation.Business.ValidationRules
{
    public class ValidateCpr : IValidationRule
    {
        private readonly Regex _regex = new(@"^[0-9]{10}");

        public ValidationError? Validate(ReceiptDetail receiptDetail, ConcurrentBag<ValidationError> existingErrors)
        {
            const string errorMessage = "Ugyldigt Cpr-nummer";
            var cpr = receiptDetail.Cpr.Replace("-", "");
            if (string.IsNullOrWhiteSpace(cpr) || !IsValidCpr(cpr))
            {
                return ValidationHelper.AddValidationError(receiptDetail, existingErrors, ErrorCode.InvalidCpr,
                    errorMessage);
            }
            else
            {
                ValidationHelper.ResolveValidationError(receiptDetail, existingErrors, ErrorCode.InvalidCpr);
                return null; // No error found, return null
            }
        }

        private bool IsValidCpr(string cpr)
        {
            if (!_regex.IsMatch(cpr))
            {
                return false; // CPR must be 10 digits
            }
            var dateString = cpr[..6];
            return ValidationHelper.IsValidDate(dateString);
        }
    }
}
