using AspireOrchestrator.Messaging.Business;
using AspireOrchestrator.Transfer.Models;

namespace AspireOrchestrator.BackendSimulator
{
    public class BackendReplyQueueManager(ILoggerFactory loggerFactory) : FileQueueManager<BackendReply, TransferBase>(loggerFactory)
    {
    }
}
