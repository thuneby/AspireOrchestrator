using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AspireOrchestrator.Transfer.WebApi.Controllers;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.ScenarioTests.Processors
{
    public class TestTransferProcessor(TransferController transferController, ILoggerFactory loggerFactory): IProcessor
    {
        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            var documentType = entity.DocumentType;
            var transferDocument = documentType == DocumentType.IpStandard;

            var result = transferDocument ? await transferController.TransferDocumentAsync(entity) : await transferController.TransferAllAsync(entity);
            if (result.Result is OkObjectResult okResult) return okResult.Value as EventEntity;
            entity.UpdateProcessResult(entity.Result, EventState.Error);
            return entity;

        }
    }
}
