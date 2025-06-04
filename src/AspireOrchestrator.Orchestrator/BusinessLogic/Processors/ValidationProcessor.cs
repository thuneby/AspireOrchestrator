using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ValidationProcessor(ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<ValidationProcessor> _logger = loggerFactory.CreateLogger<ValidationProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            // Todo: Implement the logic
            _logger.LogInformation("Validating file");
            await Task.Delay(1);
            entity.UpdateProcessResult();
            return entity;
        }
    }
}
