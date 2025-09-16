using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Domain.Models
{
    public enum TransferStatus
    {
        [Display(Name = "Ikke sendt")]
        New = 0,
        [Display(Name = "Sendt")]
        Sent = 1,
        [Display(Name = "Placeret")]
        Accepted = 2,
        [Display(Name = "Afvist")]
        Error = -1
    }
}
