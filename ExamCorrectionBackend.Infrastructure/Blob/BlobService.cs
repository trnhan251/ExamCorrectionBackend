using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ExamCorrectionBackend.Application.Contracts.Infrastructure;
using ExamCorrectionBackend.Application.Models;

namespace ExamCorrectionBackend.Infrastructure.Blob
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobInformation> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("exams");
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return new BlobInformation(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("exams");
            var items = new List<string>();
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }
            return items;
        }

        public Task UploadFileBlobAsync(string filePath, string fileName)
        {
            throw new System.NotImplementedException();
        }

        public Task UploadContentBlobAsync(string content, string fileName)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteBlobAsync(string blobName)
        {
            throw new System.NotImplementedException();
        }
    }
}