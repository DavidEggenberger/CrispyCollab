using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace WebServer.SignalR
{
    public static class SignalRDIRegistrator
    {
        public static IServiceCollection RegisterSignalR(this IServiceCollection services)
        {
            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddScoped<ISignalRHub, NotificationHub>();

            return services;
        }
    }
}
