using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ProcessFileProcessor(ILoggerFactory loggerFactory): IProcessor
    {
        private readonly ILogger<ProcessFileProcessor> _logger = loggerFactory.CreateLogger<ProcessFileProcessor>();

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
