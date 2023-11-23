using Converter.Application.Common;
using Converter.Application.Contracts;
using Converter.Application.Requests;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Converter.Application.Handlers
{
    public class XmlToJsonRequestHandler : IRequestHandler<XmlToJsonRequest, string>
    {
        private readonly IFileService _fileService;
        private readonly IXMLConverter _xmlConverter;
        private readonly IConfiguration _configuration;
        private readonly ILogger<XmlToJsonRequestHandler> _logger;

        public XmlToJsonRequestHandler(IFileService fileService, IXMLConverter xmlConverter,
            IConfiguration config, ILogger<XmlToJsonRequestHandler> logger)
        {
            _fileService = fileService;
            _xmlConverter = xmlConverter;
            _configuration = config;
            _logger = logger;
        }

        public async Task<string> Handle(XmlToJsonRequest request, CancellationToken cancellationToken)
        {
             var jsonString = await _xmlConverter.ConvertXMLtoJson(request.FileContent);

            var newFileName = Path.GetFileNameWithoutExtension(request.FileName);
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + _configuration[Constants.OutputDirectory], newFileName + Constants.JsonFileExtension);

            await _fileService.WriteToFileAsync(filePath, jsonString);

            return jsonString;
        }
    }
}
