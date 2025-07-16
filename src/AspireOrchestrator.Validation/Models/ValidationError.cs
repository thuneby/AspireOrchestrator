using System.ComponentModel.DataAnnotations;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Validation.Models
{
    public class ValidationError: GuidModelBase
    {
        public Guid? ReceiptDetailId { get; set; }
        
        [Display(Name = "Fejlkode")]
        public ErrorCode ErrorCode { get; set; }
        [Display(Name = "Fejlmeddelelse")]
        public string ErrorMessage { get; set; }
        [Display(Name = "Rettet")]
        public bool IsFixed { get; set; }

        [Display(Name = "Ignorér")]
        public bool Override { get; set; }
    }
}
