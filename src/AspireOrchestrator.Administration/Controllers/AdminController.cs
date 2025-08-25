using AspireOrchestrator.Administration.Services;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Storage.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AspireOrchestrator.Administration.Controllers
{
    public class AdminController(IStorageHelper storageHelper, EventPublisherService eventPublisherService) : Controller
    {
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

            try
            {
                await UploadFileToBlob(content, documentType, fileName);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("file", $"File upload error: {ex.Message}");
                return View("Index");
            }

            var eventType = ProcessHelper.GetEventType(documentType);

            return eventType switch
            {
                EventType.HandleReceipt => RedirectToAction("Index", "ReceiptDetail"),
                //EventType.HandleInvoice => RedirectToAction("Index", "Invoice"),
                EventType.HandleDeposit => RedirectToAction("Index", "Deposit"),
                _ => RedirectToAction("Index", "ReceiptDetail")
            };

            //const string result = "File uploaded";
            //return new ObjectResult(result);

        }

        private async Task UploadFileToBlob(byte[] content, DocumentType documentType, string fileName)
        {
            using var payload = new MemoryStream(content);
            var parameters = await storageHelper.UploadFile(payload, fileName, documentType);
            await PutEvent(documentType, parameters);
        }

        private async Task PutEvent(DocumentType documentType, string parameters)
        {
            var receiveEvent = new EventEntity
            {
                EventType = ProcessHelper.GetEventType(documentType),
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
