using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspireOrchestrator.Domain.Interfaces;

namespace AspireOrchestrator.Domain.Models
{
    public class ReceiptDetail: ReceiptDetailBase, ITransactionDocument
    {
        [Display(Name = "Indlæsningdato")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Overenskomst")]
        public string LaborAgreementNumber { get; set; } = "";

        [Required]
        [StringLength(8)]
        [Display(Name = "Cvr")]
        public string Cvr { get; set; } = "";

        [StringLength(35)]
        [Display(Name = "Navn")]
        public string PersonFullName { get; set; } = "";

        [StringLength(35)]
        [Display(Name = "Betalingsreference")]
        public string PaymentReference { get; set; } = "";

        [Display(Name = "Betalingsdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Indberetningstype")]
        public ReceiptType ReceiptType { get; set; }

        [Display(Name = "Policenummer")]
        public long? PolicyNumber { get; set; }

        [Display(Name = "Batchnummer")]
        public string BatchNumber { get; set; } = "";

        [Display(Name = "Transnr")]
        public int? TransactionNumber { get; set; }

        [Display(Name = "Samlet_bidragsprocent")]
        public decimal? TotalContributionRate { get; set; }

        [Display(Name = "Arbejdsgivers_bidragsprocent")]
        public decimal? EmployerContributionRate { get; set; }

        [Display(Name = "Arbejdsgiverbidrag")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal? EmployerContribution { get; set; }

        [Display(Name = "Bidragsprocentdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ContributionRateFromDate { get; set; }

        [Display(Name = "Normalbidrag")]
        public decimal? NormalContribution { get; set; }

        [Display(Name = "Startdato_normalbidrag")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? NormalContributionStartDate { get; set; }

        [Display(Name = "Fratrædelsesdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? EmploymentTerminationDate { get; set; }

        [Display(Name = "Afvigelse_startdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DeviationStartDate { get; set; }

        [Display(Name = "Afvigelse_slutdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DeviationEndDate { get; set; }

        [Display(Name = "Afvigelseskode")]
        public DeviationCode? DeviationCode { get; set; }

        [Display(Name = "Startdato_Pensionsgivende_løn")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? EmployeeSalaryStartDate { get; set; }

        [Display(Name = "Pensionsgivende_løn")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal? EmployeeSalary { get; set; }

        [Display(Name = "Aflønningsform")]
        public TermsOfSalary? TermsOfSalary { get; set; }

        [Display(Name = "Startdato_beskæftigelsesgrad")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? EmploymentRateStartDate { get; set; }

        [Display(Name = "Beskæftigelsesgrad")]
        public decimal? EmploymentRate { get; set; }

        [Display(Name = "Kundenummer")]
        public string CustomerNumber { get; set; } = "";

        [Display(Name = "Dannet_dato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? SubmissionDate { get; set; }

    }
}
