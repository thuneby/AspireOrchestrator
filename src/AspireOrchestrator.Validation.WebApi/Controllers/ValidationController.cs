using System.Text.Json;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Validation.Business;
using AspireOrchestrator.Validation.DataAccess;
using Microsoft.AspNetCore.Mvc;
using static System.String;

namespace AspireOrchestrator.Validation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController(
        ReceiptDetailRepository receiptDetailRepository,
        ValidationErrorRepository validationErrorRepository,
        ILoggerFactory loggerFactory) : ControllerBase
    {
        private readonly ILogger<ValidationController> _logger = loggerFactory.CreateLogger<ValidationController>();
        private readonly Validator _validator = new(validationErrorRepository);

        [HttpGet("[action]")]
        public ActionResult Welcome()
        {
            return Ok("Welcome to the validation controller");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> ValidateDocumentAsync([FromBody] EventEntity eventEntity)
        {
            var dictionary = eventEntity.GetParameterDictionary();
            var id = dictionary.GetValueOrDefault("documentId");
            if (IsNullOrEmpty(id))
            {
                _logger.LogError("Invalid parameters for event: {@EventEntity}", eventEntity);
                eventEntity.ErrorMessage = "Parameters must contain 'documentId'";
                const string message = "Validation failed due to missing document id parameter";
                eventEntity.UpdateProcessResult(message, EventState.Error);
                return NotFound(eventEntity);
            }
            var documentId = Guid.Parse(id.Trim().ToUpperInvariant());
            var receiptDetails = (await receiptDetailRepository.GetByDocumentIdAsync(documentId)).ToList();
            if (receiptDetails.Count > 0 && receiptDetails.Any(x => !x.Valid))
            {
                
                var invalid = receiptDetails.Where(x => !x.Valid).ToList();
                var validationResult = await _validator.ValidateManyAsync(invalid);
                foreach (var result in validationResult)
                {
                    var receiptDetail = receiptDetails.First(x => x.Id == result.receiptDetailId);
                    receiptDetail.Valid = result.valid;
                    receiptDetail.ValidationErrors = result.validationErrors.Count == 0
                        ? string.Empty
                        : result.validationErrors.ToString() ?? string.Empty;
                }
                await receiptDetailRepository.UpdateRange(invalid);
            }

            UpdateResult(eventEntity, dictionary, id);
            return Ok(eventEntity);
        }

        private static void UpdateResult(EventEntity eventEntity, Dictionary<string, string> dictionary, string id)
        {
            dictionary.Add("validation", "document with id " + id + "  validated");
            var jsonResult = JsonSerializer.Serialize(dictionary);
            eventEntity.UpdateProcessResult(jsonResult);
        }
    }
}
