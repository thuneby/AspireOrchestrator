using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Domain.Models
{
    public enum TermsOfSalary
    {
        [Display(Name = "Månedlig bagud")]
        Monthly = 0,
        [Display(Name = "Timeløn afregn månedlig")]
        HourlyByMonth = 1,
        [Display(Name = "14-dagesløn")]
        Biweekly = 2,
        [Display(Name = "Ugeløn")]
        Weekly = 3,
        [Display(Name = "Dagløn")]
        Daily = 4,
        [Display(Name = "Kvartårlig")]
        Quarterly = 5,
        [Display(Name = "Månedlig forud")]
        MonthlyInAdvance = 6,
        [Display(Name = "Ukendt")]
        Unknown = 99,
    }
}
