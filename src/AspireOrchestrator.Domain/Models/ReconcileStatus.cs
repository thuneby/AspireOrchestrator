using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Domain.Models
{
    public enum ReconcileStatus
    {
        [Display(Name = "Ny")]
        Open = 0,
        [Display(Name = "Betalt")]
        Paid = 1,
        [Display(Name = "Overført")]
        Sent = 2,
        [Display(Name = "Lukket")]
        Closed = 3,
        [Display(Name = "Annulleret")]
        Void = 4,
        [Display(Name = "Betaling Bekræftet")]
        Confirmed = 5
    }
}
