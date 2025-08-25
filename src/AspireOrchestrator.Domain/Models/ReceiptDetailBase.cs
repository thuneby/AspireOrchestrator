using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Domain.Models
{
    public abstract class ReceiptDetailBase : GuidModelBase
    {
        [Display(Name = "Periode fra")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Periode til")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ToDate { get; set; }

        [StringLength(11)]
        [Display(Name = "Cpr")]
        public string Cpr { get; set; } = "";

        [Display(Name = "Beløb")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Amount { get; set; }

        public bool Valid { get; set; }

        public long PersonId { get; set; }

        [Display(Name = "Afstemt")]
        public ReconcileStatus ReconcileStatus { get; set; } = ReconcileStatus.Open;

        [Display(Name = "DokumentId")]
        public Guid DocumentId { get; set; }

        public string ValidationErrors { get; set; } = "";
    }
}
