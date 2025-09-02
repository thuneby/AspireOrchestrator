using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Transfer.Business;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AspireOrchestrator.Transfer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController(TransferEngine transferEngine, ILoggerFactory loggerFactory) : ControllerBase
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<TransferController>();
        
        [HttpGet("[action]")]
        public ActionResult Welcome()
        {
            return Ok("Welcome to the transfer controller");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> TransferDocumentAsync([FromBody] EventEntity eventEntity)
        {
            var dictionary = eventEntity.GetParameterDictionary();
            var id = dictionary.GetValueOrDefault("documentId");
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("Invalid parameters for event: {@EventEntity}", eventEntity);
                eventEntity.ErrorMessage = "Parameters must contain 'documentId'";
                const string message = "Validation failed due to missing document id parameter";
                eventEntity.UpdateProcessResult(message, EventState.Error);
                return NotFound(eventEntity);
            }
            var documentId = Guid.Parse(id.Trim().ToUpperInvariant());
            var transferCount = await transferEngine.TransferDocumentAsync(documentId);
            UpdateResult(eventEntity, dictionary, transferCount);
            return Ok(eventEntity);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> TransferAllAsync([FromBody] EventEntity eventEntity)
        {
            var dictionary = eventEntity.GetParameterDictionary();
            var transferCount = await transferEngine.TransferAllAsync();
            UpdateResult(eventEntity, dictionary, transferCount);
            return Ok(eventEntity);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> HandleRepliesAsync([FromBody] EventEntity eventEntity)
        {
            var dictionary = eventEntity.GetParameterDictionary();
            var transferCount = await transferEngine.HandleReplies();
            UpdateResult(eventEntity, dictionary, transferCount, " replies handled");
            return Ok(eventEntity);
        }

        private static void UpdateResult(EventEntity eventEntity, Dictionary<string, string> dictionary, int transferCount, string message = " transfers created")
        {
            dictionary.Add("result", transferCount.ToString() + message);
            var jsonResult = JsonSerializer.Serialize(dictionary);
            eventEntity.UpdateProcessResult(jsonResult);
        }
    }
}
