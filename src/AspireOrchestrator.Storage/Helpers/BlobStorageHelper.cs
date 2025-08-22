using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Storage.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AspireOrchestrator.Storage.Helpers
{
    public class BlobStorageHelper(BlobServiceClient blobServiceClient, ILoggerFactory loggerFactory) : IStorageHelper
    {
        private const string ContainerName = "fileuploads";
        private readonly ILogger _logger = loggerFactory.CreateLogger<BlobStorageHelper>();

        public async Task<string> UploadFile(Stream fileStream, string fileName, DocumentType documentType)
        {
            var docsContainer = blobServiceClient.GetBlobContainerClient(ContainerName);
            await docsContainer.CreateIfNotExistsAsync();
            var fileId = Guid.NewGuid().ToString();
            var fileLength = fileStream.Length;

            IDictionary<string, string> metadata = new Dictionary<string, string>
            {
                { "id", fileId},
                { "documentType", documentType.ToString() },
                { "size", fileLength.ToString()},
                { "fileName", fileName },

            };

            // upload the file to Azure Blob Storage
            await docsContainer.UploadBlobAsync(fileId, fileStream);

            // set metadata for the uploaded file
            var blobClient = docsContainer.GetBlobClient(fileId);
            var uri = blobClient.Uri.ToString();
            //await blobClient.SetMetadataAsync(metadata);
            // consider using Cosmos DB for metadata storage instead of blob metadata
            // publish an event for the uploaded file
            metadata.Add("uri", uri);
            var parameters = JsonSerializer.Serialize(metadata);
            return parameters;
        }

        public async Task<Stream> GetPayload(string fileName)
        {
            var docsContainer = blobServiceClient.GetBlobContainerClient(ContainerName);
            var client = docsContainer.GetBlobClient(fileName);
            if (await client.ExistsAsync())
            {
                var result = await client.DownloadContentAsync();
                return result.Value.Content.ToStream();
            }
            else
            {
                _logger.LogError("File with ID {FileId} does not exist in blob storage", fileName);
                throw new FileNotFoundException($"File with ID {fileName} not found in blob storage");
            }
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetFileList()
        {
            throw new NotImplementedException();
        }
    }
}
