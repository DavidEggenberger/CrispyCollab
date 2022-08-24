using Microsoft.AspNetCore.Builder;

namespace WebServer.Modules.Logging
{
    public static class LoggingMdwRegistrator
    {
        public static IApplicationBuilder UseLoggingModule(this IApplicationBuilder application)
        {
            return application.UseHttpLogging();
        }
    }
}
