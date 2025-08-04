using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.MasterData.Models
{
    public class Person : TenantModelBase
    {
        public Person()
        {
            Policies = new HashSet<Policy>();
        }

        [Display(Name = "Kundenummer")]
        public long CustomerNumber { get; set; }

        [Display(Name = "Navn")]
        public string Name { get; set; } = "";

        [StringLength(11)]
        [Required]
        public string Cpr { get; set; } = "";
        public ICollection<Policy> Policies { get; set; }

    }
}
