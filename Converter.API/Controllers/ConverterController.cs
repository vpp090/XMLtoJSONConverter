using Converter.Application.Contracts;
using Converter.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        
        public async Task<ActionResult<IFormFile>> ConvertXMLtoJSON([FromForm] IFormFile xmlFile, [FromForm]string fileName)
        {
            var result = await _mediator.Send(new XmlToJsonRequest { File = xmlFile, FileName = fileName });

            if (string.IsNullOrEmpty(result))
                return BadRequest("File not processed successfully try again");

            return Ok(result);
        }
    }
}
