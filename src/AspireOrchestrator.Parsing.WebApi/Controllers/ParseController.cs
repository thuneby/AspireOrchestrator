using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.DataAccess;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

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

            // Simulate parsing logic
            var fileInfo = GetFileInfoFromParameters(eventEntity);
            if (!fileInfo.TryGetValue("id", out var fileId))
            {
                _logger.LogError("Invalid parameters for event: {@EventEntity}", eventEntity);
                throw new ArgumentException("Parameters must contain 'id''");
            }

            eventEntity.StartTime = DateTime.Now;
            eventEntity.Result = "Parsing completed successfully";
            eventEntity.EventState = EventState.Completed;
            eventEntity.ExecutionCount++;
            eventEntity.EndTime = DateTime.Now.AddSeconds(1); // Simulate processing time
            return eventEntity;
        }

        private Dictionary<string, string> GetFileInfoFromParameters(EventEntity eventEntity)
        {
            var parameters = eventEntity.Parameters;
            if (string.IsNullOrWhiteSpace(parameters))
            {
                _logger.LogError("Parameters are empty for event: {@EventEntity}", eventEntity);
                throw new ArgumentException("Parameters cannot be empty");
            }

            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(parameters);
            return dictionary ?? throw new ArgumentException("Parameters must be a valid JSON object");
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
