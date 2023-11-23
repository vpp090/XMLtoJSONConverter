
using Converter.Application.Models;
using MediatR;

namespace Converter.Application.Requests
{
    public class FileNameRequest : IRequest<FileResult>
    {
        public string FileName { get; set; }  
    }
}
