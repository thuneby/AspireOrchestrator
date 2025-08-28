using AspireOrchestrator.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Transfer.Models
{
    public class TransferBase: GuidModelBase
    {
        [Display(Name = "Betalingsdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BankTrxDate { get; set; }

        [Display(Name = "Valørdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BankValDate { get; set; }

        [Display(Name = "Periode fra")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Periode til")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Beløb")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Amount { get; set; }

        [Display(Name = "Policenummer")]
        public string PolicyNumber { get; set; } = "";

        [Display(Name = "Aftalenummer")]
        public string AgreementNumber { get; set; } = "";

        [StringLength(35)]
        [Display(Name = "Betalingsid")]
        public string PaymentReference { get; set; } = "";

        [Display(Name = "Besked")]
        public string Payload { get; set; }

        public TransferStatus TransferStatus { get; set; }

    }
}
