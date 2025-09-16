using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Storage.Interfaces;

namespace AspireOrchestrator.Storage.Helpers
{
    public class FileStorageHelper: IStorageHelper
    {
        public Task<string> UploadFile(Stream fileStream, string fileName, DocumentType documentType)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetPayload(string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetFileList()
        {
            throw new NotImplementedException();
        }
    }
}
