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
            try
            {
                var result = await ServiceInvoker.InvokeService<EventEntity, EventEntity>(HttpMethod.Post, "parseapi",
                    "api/parse/parseasync", entity);
                result.UpdateProcessResult();
                return result;
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
