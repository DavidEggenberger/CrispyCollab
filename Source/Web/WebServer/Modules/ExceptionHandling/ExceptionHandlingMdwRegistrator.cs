using Microsoft.AspNetCore.Builder;

namespace WebServer.Modules.ExceptionHandling
{
    public static class ExceptionHandlingMdwRegistrator
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseExceptionHandler("/exceptionHandler");
        }
    }
}
