using MediatR;


namespace Converter.Application.Requests
{
    public class XmlToJsonRequest : IRequest<string>
    {
        public string FileContent { get; set; }
        public string FileName { get; set; }
    }
}
