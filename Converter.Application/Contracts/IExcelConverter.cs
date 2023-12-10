namespace Converter.Application.Contracts
{
    public interface IExcelConverter
    {
        Task<string> ConvertExceltoXML(string excelContent);
    }
}