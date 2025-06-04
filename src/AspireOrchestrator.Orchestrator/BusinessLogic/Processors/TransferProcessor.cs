using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class TransferProcessor(ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<TransferProcessor> _logger = loggerFactory.CreateLogger<TransferProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            // Todo: Implement the logic
            _logger.LogInformation("Transferring result");
            await Task.Delay(1);
            entity.UpdateProcessResult();
            return entity;
        }
    }
}
