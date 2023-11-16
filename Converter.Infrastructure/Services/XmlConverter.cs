using Converter.Application.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Xml;

namespace Converter.Infrastructure.Services
{
    public class XmlConverter : IXMLConverter
    {
        private readonly ILogger<XmlConverter> _logger;
        public XmlConverter(ILogger<XmlConverter> logger)
        {
            _logger = logger;
        }
        public async Task<string> ConvertXMLtoJson(string xmlContent)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);
                var jsonString = await Task.Run(() => JsonConvert.SerializeXmlNode(xmlDoc));


                return jsonString;
            }
            catch(XmlException ex)
            {
                 _logger.LogError($"Error_In_Converting_XML:{ex}");    
                throw;
            }
           
        }
    }
}
