using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ConvertDocumentProcessor(ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<ConvertDocumentProcessor> _logger = loggerFactory.CreateLogger<ConvertDocumentProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            // Todo: Implement the logic
            _logger.LogInformation("Converting Document");
            await Task.Delay(1);
            entity.UpdateProcessResult();
            return entity;
        }
    }
}
