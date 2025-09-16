using AspireOrchestrator.Administration.Models;
using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        [HttpPost]
        public IActionResult PostingAccountBalance(DateTime balanceDate)
        {
            balanceDate = balanceDate == DateTime.MinValue ? DateTime.Now : balanceDate;
            var result = repository.GetPostingAccountSummary(balanceDate).Where(x => x.TotalBalance != 0M);
            var model = new PostingAccountBalanceViewModel
            {
                AccountBalances = result,
                BalanceDate = balanceDate
            };
            return View("PostingAccountBalance", model);
        }

        public IActionResult PostingEntryBalances(AccountType type, string postingAccount, DateTime balanceDate)
        {
            var result = repository.GetPostingEntryBalances(type, postingAccount, balanceDate);
            return View(result);
        }
    }
}
