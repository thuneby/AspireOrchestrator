using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Parsing.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AspireOrchestrator.ScenarioTests.Processors
{
    internal class TestParseProcessor(ParseController parseController) : IProcessor
    {
        public async Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            var result = await parseController.ParseAsync(entity);
            if (result.Result is OkObjectResult okResult) return okResult.Value as EventEntity;
            entity.UpdateProcessResult(entity.Result, EventState.Error);
            return entity;

        }
    }
}
