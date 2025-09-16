using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class GenerateReceiptProcessor(ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<GenerateReceiptProcessor> _logger = loggerFactory.CreateLogger<GenerateReceiptProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            // Todo: Implement the logic
            _logger.LogInformation("Generating receipt");
            await Task.Delay(1);
            entity.UpdateProcessResult(entity.Parameters);
            return entity;
        }
    }
}
