using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AspireOrchestrator.Parsing.Business;
using AspireOrchestrator.Storage.Interfaces;

namespace AspireOrchestrator.Parsing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParseController(
        IStorageHelper storageHelper,
        ReceiptDetailRepository receiptDetailRepository,
        DepositRepository depositRepository,
        ILoggerFactory loggerFactory) : ControllerBase
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
            eventEntity.StartEvent();

            // Simulate parsing logic
            var fileInfo = eventEntity.GetParameterDictionary();
            if (!fileInfo.TryGetValue("id", out var fileId))
            {
                _logger.LogError("Invalid parameters for event: {@EventEntity}", eventEntity);
                eventEntity.ErrorMessage = "Parameters must contain 'id'";
                const string message = "Parsing failed due to missing file id parameter";
                eventEntity.UpdateProcessResult(message, EventState.Error);
                return NotFound(eventEntity);
            }
            var fileStream = await GetFilestreamFromBlob(fileId);

            var jsonResult = eventEntity.DocumentType switch
            {
                DocumentType.IpStandard => await ParseReceiptDetails(eventEntity, fileStream, fileId),
                DocumentType.ReceiptDetailJson => await ParseReceiptDetails(eventEntity, fileStream, fileId),
                DocumentType.PosteringsData => await ParseDeposits(eventEntity, fileStream, fileId),
                _ => string.Empty
            };
            eventEntity.UpdateProcessResult(jsonResult);
            return Ok(eventEntity);
        }

        private async Task<string> ParseReceiptDetails(EventEntity eventEntity, Stream fileStream, string fileId)
        {
            var documentId = Guid.Parse(fileId);
            var existing = await receiptDetailRepository.GetByDocumentIdAsync(documentId);
            var count = existing.Count();
            if (count == 0) // must be idempotent
            {
                var parser = ParserFactory.GetReceiptDetailParser(eventEntity.DocumentType, loggerFactory);
                var receiptDetails = (await parser.ParseAsync(fileStream, eventEntity.DocumentType)).ToList();
                if (receiptDetails.Count > 0)
                {
                    count = receiptDetails.Count;
                    foreach (var receiptDetail in receiptDetails)
                    {
                        receiptDetail.DocumentId = documentId;
                        receiptDetail.TenantId = eventEntity.TenantId;
                    }
                    await receiptDetailRepository.AddRange(receiptDetails);
                }
            }
            var jsonResult = GetJsonResult(eventEntity, fileId, count);
            return jsonResult;
        }

        private async Task<string> ParseDeposits(EventEntity eventEntity, Stream fileStream, string fileId)
        {
            var parser = ParserFactory.GetDepositParser(eventEntity.DocumentType, loggerFactory);
            var deposits = (await parser.ParseAsync(fileStream, eventEntity.DocumentType)).ToList();
            if (deposits.Count > 0)
            {
                var documentId = Guid.Parse(fileId);
                foreach (var deposit in deposits)
                {
                    deposit.DocumentId = documentId;
                    deposit.TenantId = eventEntity.TenantId;
                }
                await depositRepository.AddRange(deposits);
            }
            var jsonResult = GetJsonResult(eventEntity, fileId, deposits.Count);
            return jsonResult;
        }


        private static string GetJsonResult(EventEntity eventEntity, string fileId, int documentCount)
        {
            var result = new Dictionary<string, string>
            {
                {"documentId", fileId },
                {"documentType", eventEntity.DocumentType.ToString()},
                {"documentCount", documentCount.ToString()}
            };
            var jsonResult = JsonSerializer.Serialize(result);
            return jsonResult;
        }

        private async Task<Stream> GetFilestreamFromBlob(string fileId)
        {
            return await storageHelper.GetPayload(fileId);
        }
    }
}
