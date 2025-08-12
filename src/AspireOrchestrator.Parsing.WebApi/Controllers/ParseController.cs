using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.AspNetCore.Mvc;

namespace AspireOrchestrator.Parsing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParseController(ILoggerFactory loggerFactory) : ControllerBase
    {
        private readonly ILogger<ParseController> _logger = loggerFactory.CreateLogger<ParseController>();

        [HttpGet("[action]")]
        public ActionResult Welcome()
        {
            return Ok("Welcome to the parse controller");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> ParseAsync([FromBody] EventEntity eventEntity)
        {
            _logger.LogInformation("Received parse request: {@Request}", eventEntity);

            // Simulate parsing logic

            eventEntity.StartTime = DateTime.Now;
            eventEntity.Result = "Parsing completed successfully";
            eventEntity.EventState = EventState.Completed;
            eventEntity.ExecutionCount++;
            eventEntity.EndTime = DateTime.Now.AddSeconds(1); // Simulate processing time
            return eventEntity;
        }
    }
}
