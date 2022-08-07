using Microsoft.Extensions.DependencyInjection;

namespace WebServer.Modules.ModelValidation
{
    public static class AutoMapperDIRegistrator
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(IAssemblyMarker).Assembly);
        }
    }
}
