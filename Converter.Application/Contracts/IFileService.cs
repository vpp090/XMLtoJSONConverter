using Converter.Application.Models;

namespace Converter.Application.Contracts
{
    public interface IFileService
    {
        Task WriteToFileAsync(string filePath, string content);

        Task<FileResult> GetFileAsync(string filePath, string fileName);
    }
}
