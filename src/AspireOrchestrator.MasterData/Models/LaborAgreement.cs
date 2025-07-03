using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.MasterData.Models
{
    public class LaborAgreement: TenantModelBase
    {
        [Display(Name = "Faggruppenummer")]
        public string LaborAgreementNumber { get; set; } = "";

        [Display(Name = "Faggruppenavn")]
        public string LaborAgreementName { get; set; } = "";
        
        [Display(Name = "Produkt")]
        public string Product { get; set; } = "";
        
        [Display(Name = "Bidragssats")]
        public decimal ContributionRate { get; set; }
        
        [Display(Name = "MinimumsBidrag")]
        public decimal MinimumContribution { get; set; }
        
        [Display(Name = "Minimumsalder")]
        public decimal MinAge { get; set; }
        
        [Display(Name = "Maksimal alder")]
        public decimal MaxAge { get; set; }
        public ICollection<Agreement> Agreements { get; set; } = new HashSet<Agreement>();
    }
}
