using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using WebServer.Authorization;

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
                    options.RequireClaim(ClaimConstants.TeamIdClaimType);
                    options.RequireClaim(ClaimConstants.TeamRoleClaimType, "User", "Admin");
                });
                options.AddPolicy("TeamAdmin", options =>
                {
                    options.RequireClaim(ClaimConstants.TeamIdClaimType);
                    options.RequireClaim(ClaimConstants.TeamRoleClaimType, "Admin");
                });
                options.AddPolicy("PremiumSubscriptionPlan", options =>
                {
                    options.RequireClaim("TeamSubscriptionPlanType", "Premium");
                });
                options.AddPolicy("EnterpriseSubscriptionPlan", options =>
                {
                    options.RequireClaim("TeamSubscriptionPlanType", "Enterprise");
                });
                options.AddPolicy("CreatorPolicy", policy =>
                {
                    policy.Requirements.Add(new CreatorPolicyRequirement());
                });
            });

            return services;
        }
    }
}
