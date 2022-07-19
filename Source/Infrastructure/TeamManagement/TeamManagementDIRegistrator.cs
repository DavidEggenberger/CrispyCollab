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
    public static class TeamManagementDIRegistrator
    {
        public static IServiceCollection RegisterTeamManagement(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationSchemeService, AuthenticationSchemeService>();
            services.AddScoped<TeamManager>();
            services.AddScoped<AdminNotificationManager>();

            return services;
        }
    }
}
