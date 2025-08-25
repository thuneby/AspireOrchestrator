using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;

namespace AspireOrchestrator.Administration.Controllers
{
    public class DepositController(DepositRepository repository, ILoggerFactory loggerFactory)
        : GuidModelBaseController<Deposit>(repository, loggerFactory)
    {
    }
}
