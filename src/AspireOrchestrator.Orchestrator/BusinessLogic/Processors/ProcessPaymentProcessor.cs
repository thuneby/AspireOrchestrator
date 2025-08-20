using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ProcessPaymentProcessor(ILoggerFactory loggerFactory): IProcessor
    {
        private readonly ILogger<ProcessPaymentProcessor> _logger = loggerFactory.CreateLogger<ProcessPaymentProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            // Todo: Implement the logic
            _logger.LogInformation("Receiving file");
            await Task.Delay(1);
            entity.UpdateProcessResult(entity.Parameters);
            return entity;
        }
    }
}
