using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Parsing.Models.IPModels
{
    public abstract class IpRecordBase : GuidModelBase
    {
        protected IpRecordBase() { }

        [StringLength(3)]
        public string Pensionsselskabskode { get; set; } = "";

        [StringLength(2)]
        public string Indberetningstype { get; set; } = "";

        [StringLength(5)]
        public string AfsendersKundenr { get; set; } = "";

        [StringLength(8)]
        public string DatoForDannelse { get; set; } = "";

        [StringLength(8)]
        public string Cvr { get; set; } = "";

        [StringLength(10)]
        public string Cpr { get; set; } = "";

        [StringLength(34)]
        public string Navn { get; set; } = "";

        [StringLength(8)]
        public string PeriodeStart { get; set; } = "";

        [StringLength(8)]
        public string PeriodeSlut { get; set; } = "";

        [StringLength(8)]
        public string Pensionsbidrag { get; set; } = "";

        [StringLength(1)]
        public string Fortegn { get; set; } = "";

        [StringLength(3)]
        public string Bidragskode { get; set; } = "";

        [StringLength(4)]
        public string SamletBidragsProcent { get; set; } = "";

        [StringLength(8)]
        public string DatoForBidragsProcent { get; set; } = "";

        [StringLength(5)]
        public string OverenskomstKode { get; set; } = "";

    }
}
