using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.MasterData.Models
{
    public class Policy : TenantModelBase
    {
        public long PersonId { get; set; }

        [Display(Name = "Aftale")] public long? AgreementId { get; set; }

        [StringLength(11)]
        [Display(Name = "Cpr")]
        public string Cpr { get; set; } = "";

        [Display(Name = "Produkt")] public string Product { get; set; } = "";

        [Display(Name = "Policetype")] public PolicyType Policytype { get; set; }

        public enum PolicyType
        {
            [Display(Name = "Arbejdsmarked")] Labor,
            [Display(Name = "Privat")] Private
        }
    }
}
