using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace WebShared.Authorization
{
    public static class AuthorizationDIRegistrator
    {
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("TeamUser", options =>
                {
                    options.RequireClaim(IdentityConstants.TeamIdClaimType);
                    options.RequireClaim(IdentityConstants.TeamRoleClaimType, "User", "Admin");
                });
                options.AddPolicy("TeamAdmin", options =>
                {
                    options.RequireClaim(IdentityConstants.TeamIdClaimType);
                    options.RequireClaim(IdentityConstants.TeamRoleClaimType, "Admin");
                });
                options.AddPolicy("PremiumSubscriptionPlan", options =>
                {
                    options.RequireClaim("TeamSubscriptionPlanType", "Premium");
                });
                options.AddPolicy("EnterpriseSubscriptionPlan", options =>
                {
                    options.RequireClaim("TeamSubscriptionPlanType", "Enterprise");
                });
            });

            return services;
        }
    }
}
