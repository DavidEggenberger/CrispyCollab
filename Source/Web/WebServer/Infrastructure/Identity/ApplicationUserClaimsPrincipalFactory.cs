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

            if (applicationUser.SelectedTenantId.HasValue)
            {
                var tenantByIdQuery = new GetTenantByIdQuery() { TenantId = applicationUser.SelectedTenantId.Value };
                Tenant currentTenant = await queryDispatcher.DispatchAsync<GetTenantByIdQuery, Tenant>(tenantByIdQuery);

                claims.AddRange(new List<Claim>
                {
                    new Claim(ClaimConstants.TenantPlanClaimType, ""),
                    new Claim(ClaimConstants.TenantNameClaimType, currentTenant.Name),
                    new Claim(ClaimConstants.TenantRoleClaimType, ""),
                    //new Claim(ClaimConstants.TenantIdClaimType, currentTenant.Id)
                });
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.TenantRoleClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
