using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebServer.Modules.HostingInformation
{
    public static class ServerInformationDIRegistrator
    {
        public static IServiceCollection RegisterServerInformationProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IServerInformationProvider, ServerInformationProvider>();
        }
    }
}
