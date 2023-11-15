namespace Converter.Application.Contracts
{
    public interface IFileService
    {
        Task WriteToFileAsync(string filePath, string content);
    }
}
