using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class IdentityEndpointConstants
    {
        public const string SignUp = "Identity/SignUp";
        public const string LoginPath = "Identity/Login";
        public const string LogoutPath = "api/account/Logout";
        public const string UserClaimsPath = "api/BFF/User";
    }
}
