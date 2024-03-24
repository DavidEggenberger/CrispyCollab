using Microsoft.AspNetCore.Builder;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public static class Registrator
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseExceptionHandler("/exceptionHandler");
        }
    }
}
