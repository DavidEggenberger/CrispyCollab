using Shared.Constants;
using System.Security.Claims;

namespace Shared.Exstensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserIdAsGuid(this ClaimsPrincipal claimsPrincipal)
        {
            return new Guid(claimsPrincipal.FindFirst(ClaimConstants.UserIdClaimType).Value);
        }

        public static string GetUserIdAsString(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimConstants.UserIdClaimType).Value;
        }

        public static Guid GetTenantId(this ClaimsPrincipal claimsPrincipal)
        {
            return new Guid(claimsPrincipal.FindFirst(ClaimConstants.TenantIdClaimType).Value);
        }
    }
}
