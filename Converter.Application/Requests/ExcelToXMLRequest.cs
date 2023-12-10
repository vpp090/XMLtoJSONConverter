using MediatR;
using Microsoft.AspNetCore.Http;

namespace Converter.Application.Requests
{
    public class ExcelToXMLRequest : IRequest<string>
    {
       public IFormFile File {get; set;}
    }
}