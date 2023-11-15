using Microsoft.AspNetCore.Http;

namespace Converter.Application.Contracts
{
    public interface IXMLConverter
    {
        Task<string> ConvertXMLtoJson(string xmlContent);
    }
}
