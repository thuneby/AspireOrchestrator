namespace AspireOrchestrator.Domain.Models
{
    public class PostingAccountSummary: PostingSummary
    {
        public string PostingAccount { get; set; } = string.Empty;
        public DateTime BalanceDate { get; set; }
    }
}
