using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Domain.Models
{
    public enum ReceiptType
    {
        [Display(Name = "Ukendt")]
        Unknown = 0,
        [Display(Name = "Præmie")]
        Payment = 1,
        [Display(Name = "Indskud")]
        Transfer = 2,
        [Display(Name = "Efterregulering")]
        Adjustment = 3,
        [Display(Name = "Frivilligt bidrag")]
        Volentary = 4,
        [Display(Name = "Information")]
        Information = 5,
        [Display(Name = "Supplerende bidrag")]
        Supplementory = 6,
        [Display(Name = "Supplerende efterregulering")]
        SupplementoryAdjustment = 7
    }
}
