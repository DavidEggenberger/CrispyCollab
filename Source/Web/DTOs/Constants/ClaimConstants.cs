using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace WebShared
{
    public class ClaimConstants
    {
        public const string NameClaimType = ClaimTypes.Name;
        public const string TeamRoleClaimType = "TeamRole";
        public const string TeamIdClaimType = "TeamId";
    }
}
