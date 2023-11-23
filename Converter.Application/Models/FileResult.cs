using Microsoft.AspNetCore.Http;

namespace Converter.Application.Models
{
    public class FileResult
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
