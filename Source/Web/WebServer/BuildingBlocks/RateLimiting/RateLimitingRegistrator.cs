using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.RateLimiting;

namespace Web.Server.BuildingBlocks.RateLimiting
{
    public static class RateLimitingRegistrator
    {
        public static IServiceCollection RegisterRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(policyName: "fixed", fixedLimiterOptions =>
                {
                    
                });
            });

            return services;
        }
    }
}
