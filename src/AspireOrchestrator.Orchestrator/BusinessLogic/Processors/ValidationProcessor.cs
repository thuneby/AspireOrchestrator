using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Orchestrator.Services;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ValidationProcessor(ValidateService validateClient, ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<ValidationProcessor> _logger = loggerFactory.CreateLogger<ValidationProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            try
            {
                var result = await validateClient.HandleEventAsync(entity, CancellationToken.None);
                entity.UpdateProcessResult(result.Result, result.EventState);
                return entity;
            }
            catch (Exception ex)
            {
                const string error = "Error processing event: {EventId}";
                _logger.LogError(ex, error, entity.Id);
                entity.ErrorMessage = ex.Message;
                entity.UpdateProcessResult(error, EventState.Error);
                return entity;
            }
        }
    }
}
