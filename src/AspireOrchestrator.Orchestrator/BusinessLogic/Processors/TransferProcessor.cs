using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Orchestrator.Services;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class TransferProcessor(TransferService transferClient, ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<TransferProcessor> _logger = loggerFactory.CreateLogger<TransferProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            try
            {
                var result = await transferClient.HandleEventAsync(entity, CancellationToken.None);
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
