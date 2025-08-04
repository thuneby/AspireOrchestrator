using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Validation.Models;
using System.Collections.Concurrent;
using System.Globalization;

namespace AspireOrchestrator.Validation.Business
{
    public static class ValidationHelper
    {
        public static ValidationError? AddValidationError(ReceiptDetail receiptDetail, ConcurrentBag<ValidationError> existingErrors, ErrorCode errorCode, string errorMessage)
        {
            var exitingError = existingErrors.FirstOrDefault(e => e.ReceiptDetailId == receiptDetail.Id && e.ErrorCode == errorCode);
            if (exitingError != null && (!exitingError.IsFixed || !exitingError.Override))
            {
                return null; // Error already exists and is not fixed or overridden
            }
            else
            {
                var newError = new ValidationError
                {
                    Id = Guid.NewGuid(), // Generate a new unique identifier for the error
                    TenantId = receiptDetail.TenantId, // Assuming TenantId is available in ReceiptDetail
                    ReceiptDetailId = receiptDetail.Id,
                    ErrorCode = errorCode,
                    ErrorMessage = errorMessage,
                    IsFixed = false,
                    Override = false
                };
                return newError; // Return the newly added error
            }

        }

        public static void ResolveValidationError(ReceiptDetail receiptDetail, ConcurrentBag<ValidationError> existingErrors, ErrorCode errorCode)
        {
            var errors = existingErrors.Where(e => e.ReceiptDetailId == receiptDetail.Id && e.ErrorCode == errorCode).ToList();
            foreach (var error in errors)
            {
                error.IsFixed = true; // Mark the error as fixed
            }
        }

        public static bool IsValidDate(string s)
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(s, "ddMMyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return false;
            }
            return date != DateTime.MinValue;
        }
    }
}
