using Infrastructure.Identity.Services;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Services;

namespace Infrastructure.MultiTenancy
{
    public static class TeamManagementDIRegistrator
    {
        public static IServiceCollection RegisterTeamManagement(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationSchemeService, AuthenticationSchemeService>();
            services.AddScoped<TeamManager>();

            return services;
        }
    }
}
