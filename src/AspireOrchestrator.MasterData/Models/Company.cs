using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.MasterData.Models
{
    public class Company: TenantModelBase
    {
        [Display(Name = "Navn")]
        public string CompanyName { get; set; } = "";

        [StringLength(8)]
        [Required]
        [Display(Name = "Cvr")]
        public string Cvr { get; set; } = "";
        public ICollection<Agreement> Agreements { get; set; } = new HashSet<Agreement>();
    }
}
