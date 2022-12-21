using Microsoft.AspNetCore.Builder;

namespace WebServer.Modules.Swagger
{
    public static class APIVersioningMdwRegistrator
    {
        public static IApplicationBuilder UseApiVersioningMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseApiVersioning();

            return applicationBuilder;
        }
    }
}
