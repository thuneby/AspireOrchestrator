using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Parsing.Models.Nordea
{
    public class Camt53Statement: GuidModelBase
    {
        public int StatementId { get; set; }
        public string MsgId { get; set; }
        public DateTime CreDtTm { get; set; }
        public string StmtId { get; set; }
        public string ElctrncSeqNb { get; set; }

        [StringLength(14)]
        public string AcctId { get; set; }
        public string AccCcy { get; set; }
        public string FinInstnId { get; set; }
        public decimal OpeningBalanceAmount { get; set; }
        public string OpeningBalanceCdtDbtInd { get; set; }
        public DateTime OpeningBalanceDate { get; set; }
        public decimal ClosingBalanceAmount { get; set; }
        public string ClosingBalanceCdtDbtInd { get; set; }
        public DateTime ClosingBalanceDate { get; set; }
        public virtual ICollection<Camt53TransactionEntry> Entries { get; set; }
    }
}
