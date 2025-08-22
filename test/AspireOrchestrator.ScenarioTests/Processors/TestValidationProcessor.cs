using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Validation.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.ScenarioTests.Processors
{
    public class TestValidationProcessor(ValidationController controller): IProcessor
    {
        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            var result = (await controller.ValidateDocumentAsync(entity)) ;
            if (result.Result is OkObjectResult okResult) return okResult.Value as EventEntity;
            entity.UpdateProcessResult(entity.Result, EventState.Error);
            return entity;
        }
    }
}
