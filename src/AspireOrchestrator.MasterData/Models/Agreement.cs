using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.MasterData.Models
{
    public class Agreement: TenantModelBase
    {
        [Display(Name = "Aftalenummer")]
        public int AgreementNumber { get; set; }
        [Display(Name = "Virksomhed")]
        public long? CompanyId { get; set; }

        public string Cvr { get; set; } = "";
        [Display(Name = "Faggruppe")]
        public LaborAgreement? LaborAgreement { get; set; }

        [Display(Name = "Overenskomstnummer")]
        public string LaborAgreementNumber { get; set; } = "";
        public ICollection<Policy> Policies { get; set; } = new HashSet<Policy>();
    }
}
