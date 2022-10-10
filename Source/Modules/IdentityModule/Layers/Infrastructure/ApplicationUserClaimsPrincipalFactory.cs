using Microsoft.AspNetCore.Identity;
using Common.Constants;
using System.Security.Claims;
using Infrastructure.CQRS.Query;
using Modules.Identity.Queries;
using Modules.Identity.Domain;

namespace Module.Infrastructure
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

            var claimsForUserQuery = new ClaimsForUserQuery { User = applicationUser };
            var claimsForUser = await queryDispatcher.DispatchAsync<ClaimsForUserQuery, IEnumerable<Claim>>(claimsForUserQuery);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
