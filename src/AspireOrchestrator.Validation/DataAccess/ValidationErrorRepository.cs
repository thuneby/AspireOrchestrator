using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Validation.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Validation.DataAccess
{
    public class ValidationErrorRepository(ValidationContext context, ILogger<ValidationErrorRepository> logger) : GuidRepositoryBase<ValidationError>(context, logger)
    {
        public List<ValidationError> GetByReceiptDetailId(Guid receiptDetailId)
        {
            return context.ValidationError
                .Where(x => x.ReceiptDetailId == receiptDetailId && !x.IsFixed && !x.Override)
                .ToList();
        }

        public List<ValidationError> GetOpenErrors(long tenantId)
        {
            return context.ValidationError
                .Where(x => x.TenantId == tenantId && !x.IsFixed && !x.Override).ToList();
        }
    }
}
