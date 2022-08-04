using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Common.Constants;
using System.Security.Claims;

namespace Infrastructure.Identity.Types.Overrides
{
    public class ApplicationUserClaimsPrincipalFactory<User> : IUserClaimsPrincipalFactory<User> where User : ApplicationUser
    {
        private ApplicationUserManager applicationUserManager;
        public ApplicationUserClaimsPrincipalFactory(ApplicationUserManager TeamApplicationUserManager)
        {
            this.applicationUserManager = TeamApplicationUserManager;
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
            //var membershipClaims = applicationUserManager.GetMembershipClaimsForApplicationUser(applicationUser);
            //claims.AddRange(membershipClaims);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.TenantRoleClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
