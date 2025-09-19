using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Validation.DataAccess;
using AspireOrchestrator.Validation.Interfaces;
using AspireOrchestrator.Validation.Models;

namespace Validation.Ui.Controllers
{
    public class ValidationErrorController(ValidationErrorRepository repository, ILoggerFactory loggerFactory) : GuidModelBaseController<ValidationError>(repository, loggerFactory)
    {
    }
}
