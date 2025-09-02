using AspireOrchestrator.Messaging.Business;
using AspireOrchestrator.Transfer.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Transfer.Business
{
    public class TransferQueueManager(ILoggerFactory loggerFactory) : FileQueueManager<TransferBase, BackendReply>(loggerFactory)
    {
        private string _transferQueue = "Transfers";
        private string _replyQueue = "Replies";


        public void SetTransferQueue(string queueName)
        {
            _transferQueue = queueName;
        }

        public void SetReplyQueue(string queueName)
        {
            _replyQueue = queueName;
        }

    }
}
