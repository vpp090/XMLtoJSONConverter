using Converter.API.BackgroundProcesses;
using Converter.Application.Requests;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Xml;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;

        public ConverterController(IMediator mediator, IDistributedCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<ActionResult<IFormFile>> ConvertXMLtoJSON([FromForm] IFormFile xmlFile, [FromForm] string fileName)
        {
            try
            {
                var fileContent = await ReadFileContentAsync(xmlFile);

                var resultKey = Guid.NewGuid().ToString();
                await _cache.SetStringAsync(resultKey, "Pending");

                var jobId = BackgroundJob.Enqueue<XmlToJsonConversionJob>(job => job.Process(fileContent, fileName, resultKey));

                return Accepted(new { JobId = jobId, ResultKey = resultKey});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<string>> GetCacheResult(string resultKey)
        {
            var result = await _cache.GetStringAsync(resultKey);

            if(result != null)
            {
               await _cache.RemoveAsync(resultKey);
               return Ok(result);
            }

            return NoContent();
        }

        private async Task<string> ReadFileContentAsync(IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                return await stream.ReadToEndAsync();
            }
        }
    }
}
