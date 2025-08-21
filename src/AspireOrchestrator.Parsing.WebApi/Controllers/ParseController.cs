using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.DataAccess;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AspireOrchestrator.Parsing.Business;

namespace AspireOrchestrator.Parsing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParseController(
        BlobServiceClient blobClient,
        ReceiptDetailRepository repository,
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
                return eventEntity;
                //return BadRequest(eventEntity); // Uncomment if you want to return BadRequest instead of returning the eventEntity
            }
            var fileStream = await GetFilestreamFromBlob(fileId);
            var parser = ParserFactory.GetReceiptDetailParser(eventEntity.DocumentType, loggerFactory);
            var receiptDetails = (await parser.ParseAsync(fileStream, eventEntity.DocumentType)).ToList();
            if (receiptDetails.Count > 0)
            {
                var documentId = Guid.Parse(fileId);
                foreach (var receiptDetail in receiptDetails)
                {
                    receiptDetail.DocumentId = documentId;
                    receiptDetail.TenantId = eventEntity.TenantId;
                }
                await repository.AddRange(receiptDetails);
            }
            var result = new Dictionary<string, string>
            {
                {"documentId", fileId },
                {"documentType", eventEntity.DocumentType.ToString()},
                {"receiptDetailCount", receiptDetails.Count.ToString()}
            };
            var jsonResult = JsonSerializer.Serialize(result);
            eventEntity.UpdateProcessResult(jsonResult);
            return eventEntity;
        }

        private async Task<Stream> GetFilestreamFromBlob(string fileId)
        {
            // This method should retrieve the file stream from Azure Blob Storage.
            // For now, we will return a dummy stream.
            var docsContainer = blobClient.GetBlobContainerClient("fileuploads");
            var client = docsContainer.GetBlobClient(fileId);
            if (await client.ExistsAsync())
            {
                var result = await client.DownloadContentAsync();
                return result.Value.Content.ToStream();
            }
            else
            {
                _logger.LogError("File with ID {FileId} does not exist in blob storage", fileId);
                throw new FileNotFoundException($"File with ID {fileId} not found in blob storage");
            }
        }
    }
}
