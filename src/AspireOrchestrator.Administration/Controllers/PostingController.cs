using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspireOrchestrator.Administration.Controllers
{
    public class PostingController(PostingRepository repository, ILoggerFactory loggerFactory) : GuidModelBaseController<PostingEntry>(repository, loggerFactory)
    {
        [HttpGet("[action]")]
        public IActionResult PostingSummary()
        {
            var summary = repository.GetPostingSummary();
            return View(summary);
        }

        [HttpGet("[action]")]
        public IActionResult PostingAccountSummary(DateTime? balanceDate)
        {
            var date = balanceDate ?? DateTime.UtcNow;
            var summary = repository.GetPostingAccountSummary(date);
            return View(summary);
        }

        [HttpGet("[action]")]
        public IActionResult PostingEntries(AccountType type, string postingAccount)
        {
            var result = repository.GetPostingEntries(type, postingAccount);
            return View(result);
        }
    }
}
