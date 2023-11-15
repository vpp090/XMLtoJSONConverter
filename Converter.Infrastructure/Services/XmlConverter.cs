using Converter.Application.Contracts;
using Newtonsoft.Json;
using System.Xml;

namespace Converter.Infrastructure.Services
{
    public class XmlConverter : IXMLConverter
    {
        public async Task<string> ConvertXMLtoJson(string xmlContent)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            var jsonString = await Task.Run(() => JsonConvert.SerializeXmlNode(xmlDoc));


            return jsonString;
        }
    }
}
