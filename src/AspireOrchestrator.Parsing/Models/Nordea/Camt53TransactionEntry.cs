using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Parsing.Models.Nordea
{
    public class Camt53TransactionEntry: GuidModelBase
    {
        public int EntryId { get; set; }

        public int NtryRef { get; set; }

        public decimal AmtValue { get; set; }

        public string AmtCcy { get; set; }
        public string CdtDbtInd { get; set; }
        public string Sts { get; set; }

        [DataType(dataType: DataType.Date)]
        public DateTime BookgDtDt { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValDtDt { get; set; }
        public string AcctSvcrRef { get; set; }
        public string PmtInfId { get; set; }
        public string RmtInf { get; set; }

    }
}
