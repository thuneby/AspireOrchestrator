using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspireOrchestrator.Orchestrator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController(
        IEventRepository eventRepository,
        IFlowRepository flowRepository,
        ILoggerFactory loggerFactory)
        : ControllerBase
    {
        private readonly WorkFlowProcessor _workflowProcessor = new(eventRepository, flowRepository, loggerFactory);
        private readonly ILogger<FlowController> _logger = loggerFactory.CreateLogger<FlowController>();

        [HttpGet("{id}")]
        public ActionResult<Flow> Get(Guid id)
        {
            var flow = flowRepository.Get(id);
            if (flow != null) return flow;
            _logger.LogWarning("Flow with id {Id} not found", id);
            return NotFound();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> ExecuteFlow(Guid flowId)
        {
            var entity = await _workflowProcessor.ProcessFlow(flowId);
            return entity;
        }


    }
}
