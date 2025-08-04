using System.Collections.Concurrent;
using System.Text.Json;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Validation.DataAccess;
using AspireOrchestrator.Validation.Interfaces;
using AspireOrchestrator.Validation.Models;

namespace AspireOrchestrator.Validation.Business
{
    public class Validator(ValidationErrorRepository validationErrorRepository) : IValidator
    {
        private readonly ValidationErrorRepository _validationErrorRepository = validationErrorRepository;

        private async Task<object> LoadMasterDate(ReceiptDetail receiptDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateAsync(ReceiptDetail receiptDetail)
        {
            var existingErrors = new ConcurrentBag<ValidationError>(_validationErrorRepository.GetByReceiptDetailId(receiptDetail.Id));
            var validationRules = LoadValidationRules();
            var foundErrors = new ConcurrentBag<ValidationError>();
            return await ValidateReceiptDetail(receiptDetail, validationRules, existingErrors, foundErrors);
        }

        private async Task<ValidationResult> ValidateReceiptDetail(ReceiptDetail receiptDetail, List<IValidationRule> validationRules,
            ConcurrentBag<ValidationError> existingErrors, ConcurrentBag<ValidationError> foundErrors)
        {
            foreach (var error in validationRules.Select(rule => 
                         rule.Validate(receiptDetail, existingErrors)).OfType<ValidationError>())
            {
                foundErrors.Add(error);
            }
            var updatedErrors = existingErrors.Where(x => x.IsFixed).ToList();
            if (updatedErrors.Count > 0)
            {
                await _validationErrorRepository.UpdateRange(updatedErrors);
            }
            if (foundErrors.Count > 0)
            {
                await _validationErrorRepository.AddRange(foundErrors);
            }

            var oldErrors = existingErrors.Where(x => !x.IsFixed);
            foreach (var error in oldErrors)
            {
                foundErrors.Add(error);
            }
            var valid = !foundErrors.Any(x => !x.IsFixed || !x.Override);
            var errorsOut = foundErrors.Select(error => JsonSerializer.Serialize(error)).ToList();
            var result = new ValidationResult(receiptDetail.Id, valid, errorsOut);
            return result;
        }

        public async Task<List<ValidationResult>> ValidateManyAsync(List<ReceiptDetail> receiptDetails)
        {
            var tenantId = receiptDetails.FirstOrDefault()?.TenantId; // ToDo
            var allExistingErrors = _validationErrorRepository.GetOpenErrors(tenantId.Value);
            var validationRules = LoadValidationRules();
            var validationResults = new ConcurrentBag<ValidationResult>();
            Parallel.ForEach(receiptDetails, async void (receiptDetail) =>
            {
                var existingErrors = 
                    new ConcurrentBag<ValidationError>(allExistingErrors.Where(x => x.ReceiptDetailId == receiptDetail.Id));
                var foundErrors = new ConcurrentBag<ValidationError>();
                var validationResult = await ValidateReceiptDetail(receiptDetail, validationRules, existingErrors, foundErrors);
                validationResults.Add(validationResult);
            });
            return validationResults.ToList();
        }

        private List<IValidationRule> LoadValidationRules()
        {
            return ValidationRuleFactory.GetAllValidationRules();
        }
    }
}
