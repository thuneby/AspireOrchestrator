using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.PaymentProcessing.Models
{
    public class MatchResult: GuidModelBase
    {
        public string PaymentReference { get; set; } = "";
        public List<Deposit> Deposits { get; set; } = new List<Deposit>();
        public List<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
    }
}
