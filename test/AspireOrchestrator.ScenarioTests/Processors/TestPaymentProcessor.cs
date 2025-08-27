using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.PaymentProcessing.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AspireOrchestrator.ScenarioTests.Processors
{
    internal class TestPaymentProcessor(PaymentProcessingController paymentController): IProcessor
    {
        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            var documentType = entity.DocumentType;
            var matchDocument = documentType == DocumentType.IpStandard;

            var result = matchDocument? await paymentController.MatchDocumentAsync(entity): await paymentController.MatchAllAsync(entity);
            if (result.Result is OkObjectResult okResult) return okResult.Value as EventEntity;
            entity.UpdateProcessResult(entity.Result, EventState.Error);
            return entity;

        }
    }
}
