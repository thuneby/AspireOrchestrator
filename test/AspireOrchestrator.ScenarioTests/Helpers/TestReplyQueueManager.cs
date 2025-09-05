using AspireOrchestrator.Messaging.Business;
using AspireOrchestrator.Transfer.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.ScenarioTests.Helpers
{
    public class TestReplyQueueManager(ILoggerFactory loggerFactory) : FileQueueManager<BackendReply, BackendReply>(loggerFactory)
    {
    }
}
