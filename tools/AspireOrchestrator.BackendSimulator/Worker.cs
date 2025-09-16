using AspireOrchestrator.Transfer.Models;

namespace AspireOrchestrator.BackendSimulator;

public class Worker(BackendReplyQueueManager queueManager, ILogger<Worker> logger)
    : BackgroundService
{
    private const string TransferQueueName = "Transfers";
    private const string ReplyQueueName = "Replies";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            var queueLength = queueManager.GetQueueLength(TransferQueueName);
            if (queueLength > 0)
            {
                for (var i = 0; i < queueLength; i++)
                {
                    var transfer = queueManager.Get(TransferQueueName);
                    if (transfer != null)
                    {
                        var reply = new BackendReply()
                        {
                            Id = Guid.NewGuid(),
                            TransferId = transfer.Id,
                            Success = true,
                            Message = "Processed successfully"
                        };
                        queueManager.Put(reply, ReplyQueueName);
                    }
                }
            }

            await Task.Delay(3000, stoppingToken);
        }
    }
}
