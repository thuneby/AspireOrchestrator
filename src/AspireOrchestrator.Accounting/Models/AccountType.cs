using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Accounting.Models
{
    public enum AccountType
    {
        [Display(Name = "Indgående betalinger")]
        OffsetAccount = 0,
        [Display(Name = "Afsendte betalinger")]
        SentAccount = 1,
        [Display(Name = "Bankkonti")]
        BankAccount = 2,
        [Display(Name = "Virksomhed")]
        Company = 3,
        [Display(Name = "Medlem")]
        Person = 4,
    }
}
