using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ReceiveFileProcessor(ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<ReceiveFileProcessor> _logger = loggerFactory.CreateLogger<ReceiveFileProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            // Todo: Implement the logic
            _logger.LogInformation("Receiving file");
            await Task.Delay(1);
            entity.UpdateProcessResult();
            return entity;
        }
    }
}
