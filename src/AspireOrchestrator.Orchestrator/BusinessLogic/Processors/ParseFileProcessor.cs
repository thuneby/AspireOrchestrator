using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
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
                entity.UpdateProcessResult(result.EventState);
                entity.Result = result.Result;
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing event: {EventId}", entity.Id);
                entity.ErrorMessage = ex.Message;
                entity.UpdateProcessResult(EventState.Error);
                return entity;
            }
        }
    }
}
