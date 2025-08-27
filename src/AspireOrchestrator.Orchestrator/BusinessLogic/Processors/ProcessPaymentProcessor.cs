using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Orchestrator.Services;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic.Processors
{
    public class ProcessPaymentProcessor(PaymentProcessingService paymentClient, ILoggerFactory loggerFactory): IProcessor
    {
        private readonly ILogger<ProcessPaymentProcessor> _logger = loggerFactory.CreateLogger<ProcessPaymentProcessor>();

        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            try
            {
                var result = await paymentClient.HandleEventAsync(entity, CancellationToken.None);
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
