using Microsoft.Extensions.DependencyInjection;

namespace Modules.TenantIdentity.Web.Server.SignalR
{
    public static class SignalRDIRegistrator
    {
        public static IServiceCollection RegisterSignalR(this IServiceCollection services)
        {
            services.AddSignalR();

            //services.AddSingleton<IUserIdProvider, UserIdProvider>();
            //services.AddScoped<ISignalRHub, NotificationHub>();

            return services;
        }
    }
}
