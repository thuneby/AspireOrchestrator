using AspireOrchestrator.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Administration.Models
{
    public class PostingAccountBalanceViewModel
    {
        public IQueryable<PostingAccountSummary> AccountBalances { get; set; }

        [Display(Name = "Balancedato")]
        public DateTime BalanceDate { get; set; }
    }
}
