using Converter.Application.Contracts;

namespace Converter.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task WriteToFileAsync(string filePath, string content)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            await File.WriteAllTextAsync(filePath, content);
        }
    }
}
