using Common.Constants;
using System.Security.Claims;

namespace Common.Exstensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasUserIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.UserIdClaimType);
        }

        public static Guid GetUserIdAsGuid(this ClaimsPrincipal claimsPrincipal)
        {
            string userIdClaimValue;
            if((userIdClaimValue = claimsPrincipal.FindFirst(ClaimConstants.UserIdClaimType).Value) is not null)
            {
                return new Guid(claimsPrincipal.FindFirst(ClaimConstants.UserIdClaimType).Value);
            }
            throw new Exception();
        }

        public static string GetUserIdAsString(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimConstants.UserIdClaimType).Value;
        }

        public static bool HasTenantIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.TenantIdClaimType);
        }

        public static Guid GetTenantIdAsGuid(this ClaimsPrincipal claimsPrincipal)
        {
            return new Guid(claimsPrincipal.FindFirst(ClaimConstants.TenantIdClaimType).Value);
        }

        public static string GetTenantIdAsString(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimConstants.TenantIdClaimType).Value;
        }
    }
}
