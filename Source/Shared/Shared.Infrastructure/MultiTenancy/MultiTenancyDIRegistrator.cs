using Shared.Modules.Layers.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Services;

namespace Shared.Modules.Layers.Infrastructure.MultiTenancy
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
