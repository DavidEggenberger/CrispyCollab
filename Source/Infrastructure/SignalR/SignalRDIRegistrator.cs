using Microsoft.Extensions.DependencyInjection;

namespace WebServer.SignalR
{
    public static class SignalRDIRegistrator
    {
        public static IServiceCollection RegisterSignalR(this IServiceCollection services)
        {

            return services;
        }
    }
}
