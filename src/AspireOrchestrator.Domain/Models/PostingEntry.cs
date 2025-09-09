using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Domain.Models
{
    public class PostingEntry: GuidModelBase
    {
        [Display(Name = "Konto")]
        public string PostingAccount { get; set; } = "";

        [Display(Name = "Kontotype")]
        public AccountType AccountType { get; set; }

        [Display(Name = "Dokumenttype")] 
        public DocumentType PostingDocumentType { get; set; }

        [Display(Name = "Debit")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal DebitAmount { get; set; }

        [Display(Name = "Kredit")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal CreditAmount { get; set; }

        [StringLength(3)]
        [Display(Name = "Valuta")]
        public string Currency { get; set; } = "";

        [Display(Name = "BankTransaktionsdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM yyyy}")]
        public DateTime BankTrxDate { get; set; }

        [Display(Name = "Valørdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM yyyy}")]
        public DateTime BankValDate { get; set; }

        [StringLength(256)]
        [Display(Name = "Posteringstekst")]
        public string PostingMessage { get; set; } = "";

        public Guid? DocumentId { get; set; }

        public Guid? PostingJournalId { get; set; }

        public virtual PostingJournal? PostingJournal { get; set; }
    }
}
