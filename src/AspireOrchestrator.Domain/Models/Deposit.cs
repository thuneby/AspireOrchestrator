using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspireOrchestrator.Domain.Models
{
    public class Deposit: GuidModelBase, ITransactionDocument
    {
        [Display(Name = "Kontonr")]
        [StringLength(34)]
        public string AccountNumber { get; set; }

        [Display(Name = "Trx.dato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime TrxDate { get; set; }

        [Display(Name = "Valørdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ValDate { get; set; }

        [Display(Name = "Afstemt")]
        public ReconcileStatus ReconcileStatus { get; set; } 

        [Display(Name = "Beløb")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Amount { get; set; }

        [Display(Name = "Beløb")]
        public string Belob { get; set; }

        [Display(Name = "Valuta")]
        [StringLength(3)]
        public string Currency { get; set; }

        [StringLength(35)]
        [Display(Name = "Kontoreference")]
        public string AccountReference { get; set; }

        [StringLength(35)]
        [Display(Name = "Reference")]
        public string PaymentReference { get; set; }

        [Display(Name = "Meddelelse")]
        public string Message { get; set; }

        [Display(Name = "DokumentId")]
        public Guid DocumentId { get; set; }
    }
}
