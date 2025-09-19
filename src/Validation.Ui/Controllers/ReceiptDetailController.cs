using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;

namespace Validation.Ui.Controllers
{
    public class ReceiptDetailController(ReceiptDetailRepository repository, ILoggerFactory loggerFactory): GuidModelBaseController<ReceiptDetail>(repository, loggerFactory)
    {
    }
}
