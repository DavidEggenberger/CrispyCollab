using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Services;

namespace Infrastructure.MultiTenancy
{
    public static class MultiTenancyDIRegistrator
    {
        public static IServiceCollection RegisterMultiTenancy(this IServiceCollection services)
        {
            services.AddScoped<ITenantResolver, TenantResolver>();

            return services;
        }
    }
}
