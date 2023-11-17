using Converter.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConverterController(IMediator mediator)
        {
            _mediator = mediator;      
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<ActionResult<IFormFile>> ConvertXMLtoJSON([FromForm] IFormFile xmlFile, [FromForm]string fileName)
        {
            try
            {
                var result = await _mediator.Send(new XmlToJsonRequest { File = xmlFile, FileName = fileName });

                if (string.IsNullOrEmpty(result))
                    return BadRequest("File Not Processed. Try again");

                return Ok(result);
            }
            catch(XmlException)
            {
                return BadRequest("Review your xml file");
            }
            catch(PathTooLongException)
            {
                return BadRequest("The file name is too long");
            }
            catch(Exception)
            {
                return BadRequest("Something went wrong. Try again");
            }
            
        }
    }
}
