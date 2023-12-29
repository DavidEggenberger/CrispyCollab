using Microsoft.Extensions.DependencyInjection;

namespace WebServer.Modules.Logging
{
    public static class LoggingDIRegistrator
    {
        public static IServiceCollection RegisterLoggingModule(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddHttpLogging(options =>
            {
            });
        }
    }
}
