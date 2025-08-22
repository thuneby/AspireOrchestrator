using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Storage.Interfaces;
using System.Text.Json;

namespace AspireOrchestrator.ScenarioTests.Helpers
{
    public class TestStorageHelper : IStorageHelper

    {
        private readonly string _storagePath = Path.Combine("..", "..", "..", "..", "AspireOrchestrator.ScenarioTests");
        private const string ContainerName = "TestData";
        private static readonly object Lock = new object();

        public Task<bool> DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetFileList()
        {
            var fullPath = GetFullPath(ContainerName);
            if (!Directory.Exists(fullPath))
                return new List<string>();
            var files = Directory.GetFiles(fullPath);
            var fileList = files.Select(Path.GetFileName).ToList();
            return fileList;
        }

        public async Task<Stream> GetPayload(string fileId)
        {
            var fullPath = GetFullPath(ContainerName);
            if (!Directory.Exists(fullPath))
                throw new DirectoryNotFoundException($"The directory {fullPath} does not exist.");
            var fullName = Path.Combine(fullPath, fileId);
            var fileInfo = new FileInfo(fullName);
            var content = new byte[fileInfo.Length];
            await using var stream = new FileStream(fullName, FileMode.Open, FileAccess.Read);
            await stream.ReadExactlyAsync(content, 0, (int)fileInfo.Length);
            return new MemoryStream(content);
        }

        public async Task<string> UploadFile(Stream fileStream, string fileName, DocumentType documentType)
        {
            var fullPath = Path.Combine(GetFullPath(ContainerName), fileName);
            await using var file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(file);
            var fileInfo = new FileInfo(fullPath);
            var metadata = new Dictionary<string, string>()
            {
                { "id", fileName },
                { "documentType", documentType.ToString() },
                { "size", fileInfo.Length.ToString() },
                { "fileName", fileName },
                { "uri", fullPath }
            };
            var parameters = JsonSerializer.Serialize(metadata);
            return parameters;
        }

        private string GetFullPath(string containerName)
        {
            return Path.Combine(_storagePath, containerName);
        }

        private static void CreateDirectory(string fullPath)
        {
            lock (Lock)
            {
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
            }
        }
    }
}
