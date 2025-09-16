namespace AspireOrchestrator.Domain.Models
{
    public class PostingSummary
    {
        public AccountType AccountType { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal DebitBalance { get; set; }
        public decimal CreditBalance { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
