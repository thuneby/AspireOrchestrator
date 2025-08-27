using System.Text.Json;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.PaymentProcessing.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspireOrchestrator.PaymentProcessing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProcessingController(IProcessPayment paymentProcessor, ILoggerFactory loggerFactory)
        : ControllerBase
    {
        private readonly ILogger<PaymentProcessingController> _logger =
            loggerFactory.CreateLogger<PaymentProcessingController>();

        [HttpGet("[action]")]
        public ActionResult Welcome()
        {
            return Ok("Welcome to the payment processing controller");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> MatchDocumentAsync([FromBody] EventEntity eventEntity)
        {
            _logger.LogInformation("Received payment processing request: {@Request}", eventEntity);
            try
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
                var matched = await paymentProcessor.MatchDocumentAsync(documentId);
                var matchResult = string.IsNullOrWhiteSpace(matched.PaymentReference) ? "Payment processed" : "No payment processed";
                UpdateResult(eventEntity, dictionary, matchResult);
                return Ok(eventEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment for event: {@EventEntity}", eventEntity);
                eventEntity.ErrorMessage = ex.Message;
                eventEntity.UpdateProcessResult(ex.Message, EventState.Error);
                return StatusCode(500, eventEntity);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> MatchDocumentTypeAsync([FromBody] EventEntity eventEntity)
        {
            try
            {
                var dictionary = eventEntity.GetParameterDictionary();
                var matched = await paymentProcessor.MatchDocumentTypeAsync(eventEntity.DocumentType);
                var matchResult = matched.Count > 0 ? "Payment processed" : "No payment processed";
                UpdateResult(eventEntity, dictionary, matchResult);
                return Ok(eventEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment for event: {@EventEntity}", eventEntity);
                eventEntity.ErrorMessage = ex.Message;
                eventEntity.UpdateProcessResult(ex.Message, EventState.Error);
                return StatusCode(500, eventEntity);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> MatchAllAsync([FromBody] EventEntity eventEntity)
        {
            try
            {
                var dictionary = eventEntity.GetParameterDictionary();
                var matched = await paymentProcessor.MatchAllAsync();
                var matchResult = matched.Count > 0 ? "Payment processed" : "No payment processed";
                UpdateResult(eventEntity, dictionary, matchResult);
                return Ok(eventEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment for event: {@EventEntity}", eventEntity);
                eventEntity.ErrorMessage = ex.Message;
                eventEntity.UpdateProcessResult(ex.Message, EventState.Error);
                return StatusCode(500, eventEntity);
            }
        }

        private static void UpdateResult(EventEntity eventEntity, Dictionary<string, string> dictionary, string matchResult)
        {
            dictionary.Add("matchResult", matchResult);
            var jsonResult = JsonSerializer.Serialize(dictionary);
            eventEntity.UpdateProcessResult(jsonResult);
        }
    }
}
