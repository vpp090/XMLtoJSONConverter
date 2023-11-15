using MediatR;
using Microsoft.AspNetCore.Http;


namespace Converter.Application.Requests
{
    public class XmlToJsonRequest : IRequest<string>
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }
}
