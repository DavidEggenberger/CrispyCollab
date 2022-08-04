using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SignalR
{
    public static class SignalRDIRegistrator
    {
        public static IServiceCollection RegisterSignalR(this IServiceCollection services)
        {
            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            return services;
        }
    }
}
