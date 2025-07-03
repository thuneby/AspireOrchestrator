using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Domain.Models
{
    public enum DeviationCode
    {
        [Display(Name = "Ingen")]
        None = 0,
        [Display(Name = "Værnepligt")]
        MilitaryService = 4,
        [Display(Name = "Manglende uddannelse")]
        MissingEducation = 5,
        [Display(Name = "Manglende uddannelse med 100 % stedtillæg")]
        MissingEducationWithPlacementBonus = 7,
        [Display(Name = "Reduktion")]
        Reduction = 8,
        [Display(Name = "Andet")]
        Other = 9,
        [Display(Name = "Ansættelse, ny i pensionsordning")]
        NewPolicy = 10,
        [Display(Name = "Fortsættelse indenfor samme koncern")]
        SameEnterprise = 11,
        [Display(Name = "Hjemstationering")]
        Repatriation = 12,
        [Display(Name = "Barselsorlov uden pensionsbidrag")]
        MaternityNoPension = 20,
        [Display(Name = "Barselsorlov med nedsat pensionsbidrag")]
        MaternityReducedPension = 21,
        [Display(Name = "Børnepasningsorlov uden pensionsbidrag")]
        ChildLeaveNoPension = 22,
        [Display(Name = "Børnepasningsorlov med nedsat pensionsbidrag")]
        ChildLeaveReducedPension = 23,
        [Display(Name = "Anden orlov uden pensionsbidrag")]
        OtherLeaveNoPension = 24,
        [Display(Name = "Anden orlov med nedsat pensionsbidrag")]
        OtherLeaveReducedPension = 25,
        [Display(Name = "Anden midlertidig nedsættelse af pensionsbidrag")]
        OtherTemporaryReduction = 26,
        [Display(Name = "Ferie uden pensionsbidrag")]
        VacationNoPension = 30,
        [Display(Name = "Ferie med nedsat pensionsbidrag")]
        VacationReducedPension = 31,
        [Display(Name = "Ferie og orlov slut")]
        VacationAndLeaveEnded = 40,
        [Display(Name = "Fratrædelse")]
        EmploymentTermination = 50,
        [Display(Name = "Fratrædelse, men fortsættelse indenfor samme koncern")]
        TerminationSameEnterprise = 51,
        [Display(Name = "Kontraktudløb")]
        EndOfContract = 52,
        [Display(Name = "Udstationering")]
        ExPatriation = 53,
        [Display(Name = "Suspension")]
        Suspension = 54,
        [Display(Name = "Overgået til andet lønsystem")]
        NewPayrollSystem = 55,
        [Display(Name = "Midlertidig udtrædelse af pensionsordning")]
        TemporaryExit = 56,
    }
}
