using Converter.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        private readonly IXMLConverter _converter;

        public ConverterController(IXMLConverter converter)
        {
            _converter = converter;          
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        
        public async Task<ActionResult<IFormFile>> ConvertXMLtoJSON([FromForm] IFormFile xmlFile, [FromForm]string fileName)
        {
            var result = await _converter.ConvertXMLtoJson(xmlFile, fileName);

            if (string.IsNullOrEmpty(result))
                return BadRequest("File not processed successfully try again");

            return Ok(result);
        }
    }
}
