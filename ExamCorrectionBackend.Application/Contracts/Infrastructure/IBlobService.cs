using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using ExamCorrectionBackend.Application.Models;

namespace ExamCorrectionBackend.Application.Contracts.Infrastructure
{
    public interface IBlobService
    {
        public Task<BlobInformation> GetBlobAsync(string name);
        public Task<IEnumerable<string>> ListBlobsAsync();
        public Task UploadFileBlobAsync(string filePath, string fileName);
        public Task UploadContentBlobAsync(string content, string fileName);
        public Task DeleteBlobAsync(string blobName);
    }
}