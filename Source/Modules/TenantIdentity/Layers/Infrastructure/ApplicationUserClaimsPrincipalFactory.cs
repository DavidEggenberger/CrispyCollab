using Microsoft.AspNetCore.Identity;
using Shared.SharedKernel.Constants;
using System.Security.Claims;
using Shared.Infrastructure.CQRS.Query;
using Modules.IdentityModule.Domain;
using Modules.TenantIdentityModule.Domain;

namespace Modules.TenantIdentityModule.Layers.Infrastructure
{
    public class ApplicationUserClaimsPrincipalFactory<User> : IUserClaimsPrincipalFactory<User> where User : ApplicationUser
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IQueryDispatcher queryDispatcher;
        public ApplicationUserClaimsPrincipalFactory(ApplicationUserManager applicationUserManager, IQueryDispatcher queryDispatcher)
        {
            this.applicationUserManager = applicationUserManager;
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(user.Id);

            //var claimsForUserQuery = new ClaimsForUserQuery { User = applicationUser };
            //var claimsForUserQuery = new ClaimsForUserQuery();
            //var claimsForUser = await queryDispatcher.DispatchAsync<ClaimsForUserQuery, IEnumerable<Claim>>(claimsForUserQuery);
            var claimsForUser = await applicationUserManager.GetClaimsAsync(applicationUser);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
