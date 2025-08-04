namespace AspireOrchestrator.Validation.Models
{
    public record ValidationResult(Guid receiptDetailId, bool valid, List<string> validationErrors);
}
