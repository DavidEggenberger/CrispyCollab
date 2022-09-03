using Microsoft.AspNetCore.Identity;
using Common.Constants;
using System.Security.Claims;
using Infrastructure.Identity;
using Application.TenantAggregate.Queries;
using Infrastructure.CQRS.Query;
using Domain.Aggregates.TenantAggregate;

namespace WebServer.Identity
{
    public class ApplicationUserClaimsPrincipalFactory<User> : IUserClaimsPrincipalFactory<User> where User : ApplicationUser
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IQueryDispatcher queryDispatcher;
        public ApplicationUserClaimsPrincipalFactory(ApplicationUserManager TeamApplicationUserManager, IQueryDispatcher queryDispatcher)
        {
            this.applicationUserManager = TeamApplicationUserManager;
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(user.Id);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserNameClaimType, applicationUser.UserName),
                new Claim(ClaimConstants.UserIdClaimType, applicationUser.Id.ToString()),
                new Claim(ClaimConstants.EmailClaimType, applicationUser.Email),
                new Claim(ClaimConstants.PictureClaimType, applicationUser.PictureUri)
            };

            var tenantByIdQuery = new GetTenantByQuery() { TenantId = applicationUser.SelectedTenantId };
            Tenant currentTenant = await queryDispatcher.DispatchAsync<GetTenantByQuery, Tenant>(tenantByIdQuery);

            var tenantMembershipQuery = new GetTenantMembershipQuery { TenantId = applicationUser.SelectedTenantId, UserId = applicationUser.Id };
            TenantMembership tenantMembership = await queryDispatcher.DispatchAsync<GetTenantMembershipQuery, TenantMembership>(tenantMembershipQuery);

            claims.AddRange(new List<Claim>
            {
                new Claim(ClaimConstants.TenantPlanClaimType, currentTenant.SUbscriptionPlan.ToString()),
                new Claim(ClaimConstants.TenantNameClaimType, currentTenant.Name),
                new Claim(ClaimConstants.TenantIdClaimType, currentTenant.Id.ToString()),
                new Claim(ClaimConstants.UserRoleInTenantClaimType, tenantMembership.Role.ToString()),
            });

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
