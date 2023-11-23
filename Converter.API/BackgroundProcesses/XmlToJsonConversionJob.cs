using Converter.Application.Requests;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Xml;

namespace Converter.API.BackgroundProcesses
{
    public class XmlToJsonConversionJob
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;

        public XmlToJsonConversionJob(IMediator mediator, IDistributedCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<string> Process(string fileContent, string fileName, string resultKey)
        {
            try
            {
                var result = await _mediator.Send(new XmlToJsonRequest { FileContent = fileContent, FileName = fileName });

                await _cache.SetStringAsync(resultKey, result);

                if (string.IsNullOrEmpty(result))
                {
                    throw new Exception("File Not Processed. Try again");
                }

                return result;
            }
            catch (XmlException)
            {
                throw new Exception("Review your xml file");
            }
            catch (PathTooLongException)
            {
                throw new Exception("The file name is too long");
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong. Try again");
            }
        }
    }
}
