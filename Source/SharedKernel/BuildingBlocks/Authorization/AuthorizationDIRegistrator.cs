using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using WebServer.Authorization;
using SharedKernel.Constants;

namespace SharedKernel.BuildingBlocks.Authorization
{
    public static class AuthorizationDIRegistrator
    {
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy(PolicyConstants.TenantMemberPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantIdClaimType);
                    options.RequireRole(RoleConstants.User, RoleConstants.Admin);
                });
                options.AddPolicy(PolicyConstants.TenantAdminPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantIdClaimType);
                    options.RequireRole(RoleConstants.Admin);
                });
                options.AddPolicy(PolicyConstants.ProfessionalSubscriptionPlanPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantPlanClaimType, SubscriptionPlanConstants.ProfessionalPlan);
                });
                options.AddPolicy(PolicyConstants.EnterpriseSubscriptionPlanPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantPlanClaimType, SubscriptionPlanConstants.EnterprisePlan);
                });
                options.AddPolicy(PolicyConstants.CreatorPolicy, options =>
                {
                    options.AddRequirements(new CreatorPolicyRequirement());
                });
            });

            return services;
        }
    }
}
