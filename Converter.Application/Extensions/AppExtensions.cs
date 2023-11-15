using Converter.Application.Contracts;
using Converter.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Converter.Application.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IXMLConverter, XMLConverterService>();
            
            return services;
        }
    }
}
