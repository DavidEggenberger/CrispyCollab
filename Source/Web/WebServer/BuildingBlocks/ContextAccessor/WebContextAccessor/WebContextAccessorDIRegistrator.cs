using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.BuildingBlocks.ContextAccessor.WebContextAccessor
{
    public static class WebContextAccessorDIRegistrator
    {
        public static IServiceCollection RegisterWebContextAccessor(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IWebContextAccessor, WebContextAccessor>();
        }
    }
}
