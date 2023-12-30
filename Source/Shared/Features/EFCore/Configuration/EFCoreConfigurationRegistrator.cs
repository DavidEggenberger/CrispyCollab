using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Shared.Features.EFCore.Configuration
{
    public static class EFCoreConfigurationRegistrator
    {
        public static IServiceCollection RegisterEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp =>
            {
                EFCoreConfiguration tc = new EFCoreConfiguration();
                configuration.GetSection(nameof(EFCoreConfiguration)).Bind(tc);
                return tc;
            });
            services.Configure<EFCoreConfiguration>(configuration.GetSection(nameof(EFCoreConfiguration)));
            services.AddSingleton<IValidateOptions<EFCoreConfiguration>, EFCoreConfigurationValidator>();

            return services;
        }
    }
}
