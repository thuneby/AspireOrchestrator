using System.Globalization;
using System.Text.Json;
using AspireOrchestrator.Administration.Services;
using AspireOrchestrator.Core.OrchestratorModels;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AspireOrchestrator.Administration.Controllers
{
    public class AdminController(BlobServiceClient blobClient, EventPublisherService eventPublisherService) : Controller
    {
        private readonly BlobServiceClient _blobClient = blobClient ?? throw new ArgumentNullException(nameof(blobClient));

        // Constructor logic if needed

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormFile file, int type)
        {
            if (file.Length is <= 0)
            {
                ModelState.AddModelError("file", "File is empty or too large.");
                return View("Index");
            }

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString();
            if (string.IsNullOrEmpty(fileName))
            {
                ModelState.AddModelError("file", "Invalid file type. Please upload a .txt file.");
                return View("Index");
            }

            byte[] content;

            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                content = reader.ReadBytes((int)file.Length);
            }

            var documentType = (DocumentType)type;
            var filetype = file.ContentType.ToLowerInvariant();
            var fileLength = file.Length;


            try
            {
                await UploadFileToBlob(content, documentType, fileLength, fileName, filetype);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("file", $"File upload error: {ex.Message}");
                return View("Index");
            }

            const string result = "File uploaded";
            return new ObjectResult(result);

        }

        private async Task UploadFileToBlob(byte[] content, DocumentType documentType, long fileLength, string fileName, string filetype = "")
        {
            using var payload = new MemoryStream(content);
            var docsContainer = _blobClient.GetBlobContainerClient("fileuploads");

            var fileId = Guid.NewGuid().ToString();

            IDictionary<string, string> metadata = new Dictionary<string, string>
            {
                { "id", fileId},
                { "documentType", documentType.ToString() },
                { "size", fileLength.ToString()}, 
                { "fileName", fileName },
                { "fileType", filetype }

            };

            // upload the file to Azure Blob Storage
            await docsContainer.UploadBlobAsync(fileId, payload);
                
            // set metadata for the uploaded file
            var blobClient = docsContainer.GetBlobClient(fileId);
            var uri = blobClient.Uri.ToString();
            //await blobClient.SetMetadataAsync(metadata);
            // consider using Cosmos DB for metadata storage instead of blob metadata
            // publish an event for the uploaded file
            metadata.Add("uri", uri);
            var parameters = JsonSerializer.Serialize(metadata);
            await PutEvent(documentType, parameters);
        }

        private async Task PutEvent(DocumentType documentType, string parameters)
        {
            var receiveEvent = new EventEntity
            {
                EventType = EventType.HandleReceipt,
                ProcessState = ProcessState.Parse,
                EventState = EventState.New,
                FlowId = null, // set by handling logic
                DocumentType = documentType,
                CreatedDate = DateTime.UtcNow,
                Parameters = parameters
                
            };
            await eventPublisherService.PublishEvent(receiveEvent, "events");
        }
    }
}
