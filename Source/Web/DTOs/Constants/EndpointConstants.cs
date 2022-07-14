using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EndpointConstants
    {
        public const string IdentityAccountPath = "/Identity/Account";
        public const string SignUpPath = "Identity/SignUp";
        public const string LoginPath = "Identity/Login";
        public const string LogoutPath = "api/user/Logout";
        public const string UserClaimsPath = "api/user";
        public const string NotificationHub = "/NotificationHub";
        public const string StripePremiumSubscriptionPath = "/api/stripe/subscribe/premium"; 
        public const string StripeEnterpriseSubscriptionPath = "/api/stripe/subscribe/enterprise";
    }
}
