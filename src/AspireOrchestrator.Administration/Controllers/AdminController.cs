using AspireOrchestrator.Core.OrchestratorModels;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AspireOrchestrator.Administration.Controllers
{
    public class AdminController(BlobServiceClient blobClient) : Controller
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

            IDictionary<string, string> metadata = new Dictionary<string, string>
            {
                { "documentType", documentType.ToString() },
                { "size", fileLength.ToString()}, 
                { "fileName", fileName },
                { "fileType", filetype }

            };
            // upload the file to Azure Blob Storage
            await docsContainer.UploadBlobAsync(fileName, payload);
                
            // set metadata for the uploaded file
            var blobClient = docsContainer.GetBlobClient(fileName);
            await blobClient.SetMetadataAsync(metadata);
        }

        private async Task PutEvent()
        {
            var receiveEvent = new EventEntity();

        }
    }
}
