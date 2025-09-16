using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Storage.Interfaces
{
    public interface IStorageHelper
    {
        Task<string> UploadFile(Stream fileStream, string fileName, DocumentType documentType);
        Task<Stream> GetPayload(string fileId);
        Task<bool> DeleteFile(string fileId);
        Task<IEnumerable<string>> GetFileList();
    }
}
