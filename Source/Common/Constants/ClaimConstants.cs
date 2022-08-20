using System.Security.Claims;

namespace Common.Constants
{
    public class ClaimConstants
    {
        public const string UserIdClaimType = ClaimTypes.Sid;
        public const string UserNameClaimType = ClaimTypes.Name;
        public const string EmailClaimType = ClaimTypes.Email;
        public const string PictureClaimType = ClaimTypes.Email;
        public const string TenantIdClaimType = "TenantId";
        public const string TenantRoleClaimType = "TenantRole";
        public const string TenantPlanClaimType = "TenantPlan";
    }
}
