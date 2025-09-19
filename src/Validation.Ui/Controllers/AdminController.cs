using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Validation.Ui.Controllers
{
    public class AdminController : Controller
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
            if (file == null || file.Length is <= 0)
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
                // get parser and parse file
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("file", $"File upload error: {ex.Message}");
                return View("Index");
            }

            return RedirectToAction("Index", "ReceiptDetail");


        }
    }
}
