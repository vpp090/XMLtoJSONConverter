using Converter.Application.Contracts;
using Converter.Application.Models;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Converter.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private string _directoryPath;

        public FileService(ILogger<FileService> logger) 
        {
            _logger = logger;
        }

        public async Task<FileResult> GetFileAsync(string filePath, string fileName)
        {
            if(!File.Exists(filePath))
            {
                throw new ArgumentException("File_Path_Not_Existent");
            }

            var content = await File.ReadAllBytesAsync(filePath);

            return new FileResult { FileContent = content, FileName = fileName, ContentType = GetContentType(fileName)};
        }

        public async Task WriteToFileAsync(string filePath, string content)
        {
            try
            {
                    _directoryPath = Path.GetDirectoryName(filePath);
                
                    if (!Directory.Exists(_directoryPath))
                    {
                        Directory.CreateDirectory(_directoryPath);
                    }

                    await File.WriteAllTextAsync(filePath, content);
            }
            catch(PathTooLongException ex)
            {
                _logger.LogError("Path_Is_Too_Long", ex);
                throw;
            }
            catch(IOException ex)
            {
                _logger.LogError("File_IO_Exception", ex);
                throw;
            }

        }

        private string GetContentType(string fileName)
        {
            return Path.GetExtension(fileName.ToLowerInvariant()) switch
            {
                ".txt" => "text/plain",
                ".json" => "application/json",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream",
            };
        }
    }
}
