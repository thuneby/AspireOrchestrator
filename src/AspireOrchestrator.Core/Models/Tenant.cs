using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Core.Models
{
    public class Tenant: LongEntityBase
    {
        public string TenantName { get; set; } = string.Empty;

        [StringLength(8)]
        [Display(Name = "Cvr")]
        public string PublicId { get; set; } = string.Empty;

        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Aktiv")]
        public bool IsActive { get; set; }

        [Display(Name = "Master Tenant")]
        public bool IsDefaultTenant { get; set; }

    }
}
