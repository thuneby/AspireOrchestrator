using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Domain.Models
{
    public class PostingJournal: GuidModelBase
    {
        [Display(Name = "Bogføringsdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM yyyy}")]
        public DateTime PostingDate { get; set; }

        [Display(Name = "Formål")] public string PostingPurpose { get; set; } = "";

        public ICollection<PostingEntry> PostingEntries { get; set; } = new HashSet<PostingEntry>();

    }
}
