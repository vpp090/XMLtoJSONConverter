
using Converter.Application.Common;
using Converter.Application.Contracts;
using Converter.Application.Models;
using Converter.Application.Requests;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Converter.Application.Handlers
{
    public class FileNameRequestHandler : IRequestHandler<FileNameRequest, FileResult>
    {
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public FileNameRequestHandler(IFileService fileService, IConfiguration configuration)
        {
            _fileService = fileService;
            _configuration = configuration;
        }

        public async Task<FileResult> Handle(FileNameRequest request, CancellationToken cancellationToken)
        {
            var requestFileName = request.FileName;

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + _configuration[Constants.OutputDirectory], requestFileName);

            var result = await _fileService.GetFileAsync(filePath, request.FileName);

            return result;
        }
    }
}
