using Microsoft.Extensions.DependencyInjection;
using RuomRaCoffe.Admin.Services;

namespace RuomRaCoffe.Admin.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyAppServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<StaffService>();
            return services;
        }
    }
}