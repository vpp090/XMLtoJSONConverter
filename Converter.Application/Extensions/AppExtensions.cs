using Microsoft.Extensions.DependencyInjection;


namespace Converter.Application.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(AppExtensions).Assembly));
            
            return services;
        }
    }
}
