using Converter.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private IMediator _mediator;

        public GetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{fileName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<ActionResult> GetFile(string fileName)
        {
            var result = await _mediator.Send(new FileNameRequest { FileName = fileName });

            return File(result.FileContent, result.ContentType, result.FileName);
        }
    }
}
