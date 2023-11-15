using Converter.Application.Common;
using Converter.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Xml;

namespace Converter.Application.Services
{
    public class XMLConverterService : IXMLConverter
    {
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public XMLConverterService(IFileService fileService, IConfiguration config)
        {
            _fileService = fileService;
            _configuration = config;
        }

        public async Task<string> ConvertXMLtoJson(IFormFile xmlFile, string fileName)
        {
            string jsonString = string.Empty;

            using(var streamReader = new StreamReader(xmlFile.OpenReadStream()))
            {
                var xmlContent = await streamReader.ReadToEndAsync();

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);
                jsonString = JsonConvert.SerializeXmlNode(xmlDoc);

                var newFileName = Path.GetFileNameWithoutExtension(fileName);
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + _configuration[Constants.OutputDirectory], newFileName + Constants.JsonFileExtension);
                await _fileService.WriteToFileAsync(filePath, jsonString);
            }

            return jsonString;
        }
    }
}
