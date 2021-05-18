using System.IO;

namespace ExamCorrectionBackend.Application.Models
{
    public class BlobInformation
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }

        public BlobInformation(Stream content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }
    }
}