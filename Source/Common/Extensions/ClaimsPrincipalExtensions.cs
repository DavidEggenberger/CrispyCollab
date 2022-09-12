using Common.Constants;
using System.ComponentModel;
using System.Security.Claims;

namespace Common.Exstensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasUserIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.UserIdClaimType);
        }

        public static T GetUserId<T>(this ClaimsPrincipal claimsPrincipal)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(claimsPrincipal?.FindFirst(ClaimConstants.UserIdClaimType).Value);
        }

        public static bool HasTenantIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.TenantIdClaimType);
        }

        public static T GetTenantId<T>(this ClaimsPrincipal claimsPrincipal)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(claimsPrincipal?.FindFirst(ClaimConstants.TenantPlanClaimType).Value);
        }

        public static string GetNullableClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.FindFirst(claimType)?.Value;
        }
    }
}
