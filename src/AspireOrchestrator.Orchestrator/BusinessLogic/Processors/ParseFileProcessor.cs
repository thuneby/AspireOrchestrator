using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ParseFileProcessor(ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<ParseFileProcessor> _logger = loggerFactory.CreateLogger<ParseFileProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            // Todo: Implement the logic
            _logger.LogInformation("Parsing file");
            await Task.Delay(1);
            entity.UpdateProcessResult();
            return entity;
        }
    }
}
