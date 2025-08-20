using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;
using AspireOrchestrator.Orchestrator.Services;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ParseFileProcessor(ParseService parseClient, ILoggerFactory loggerFactory) : IProcessor
    {
        private readonly ILogger<ParseFileProcessor> _logger = loggerFactory.CreateLogger<ParseFileProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            try
            {
                var result = await parseClient.HandleEventAsync(entity, CancellationToken.None);
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
