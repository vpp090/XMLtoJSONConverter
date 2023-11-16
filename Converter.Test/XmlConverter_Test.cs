

using Converter.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Xml;

namespace Converter.Test
{
    [TestFixture]
    public class XmlConverter_Test
    {
        private XmlConverter _converter;
        private Mock<ILogger<XmlConverter>> _logger;
        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<XmlConverter>>();
            _converter = new XmlConverter(_logger.Object);
        }

        [Test]
        public async Task ConvertXMLtoJson_Pass()
        {
            var xmlContent = "<root><item>value</item></root>";
            string jsonString = "{\"root\":{\"item\":\"value\"}}";

            var result = await _converter.ConvertXMLtoJson(xmlContent);

            Assert.That(result, Is.EqualTo(jsonString));
        }

        [Test]
        public void ConvertXMLToJson_Fail()
        {
            var xmlContent = "root><item>value</item></root>";
           
            var ex = Assert.ThrowsAsync<XmlException>(async () => await  _converter.ConvertXMLtoJson(xmlContent));
            
            Assert.That(ex, Is.TypeOf<XmlException>());
        }
    }
}