using Shared.Constants;
using System.Security.Claims;

namespace Shared.Exstensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return new Guid(claimsPrincipal.FindFirst(ClaimConstants.UserIdClaimType).Value);
        }
    }
}
