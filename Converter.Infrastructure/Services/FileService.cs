using Converter.Application.Contracts;

namespace Converter.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task WriteToFileAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }
}
