using Infrastructure.Identity.Services;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Services;

namespace Infrastructure.MultiTenancy
{
    public static class MultiTenancyDIRegistrator
    {
        public static IServiceCollection RegisterMultiTenancy(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationSchemeService, AuthenticationSchemeService>();
            services.AddScoped<ITenantResolver, TeamResolver>();
            services.AddScoped<TeamManager>();
            services.AddScoped<AdminNotificationManager>();

            return services;
        }
    }
}
