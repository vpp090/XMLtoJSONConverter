using Converter.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Converter.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        public FileService(ILogger<FileService> logger) 
        {
            _logger = logger;
        }

        public async Task WriteToFileAsync(string filePath, string content)
        {
            try
            {
                    var directoryPath = Path.GetDirectoryName(filePath);
                
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
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
    }
}
