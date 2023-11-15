using Converter.Application.Contracts;
using Converter.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Converter.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
