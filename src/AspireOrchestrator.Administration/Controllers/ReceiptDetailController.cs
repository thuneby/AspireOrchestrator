using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Administration.Controllers
{
    public class ReceiptDetailController(ReceiptDetailRepository repository, ILoggerFactory loggerFactory) : GuidModelBaseController<ReceiptDetail>(repository, loggerFactory)
    {
    }
}
