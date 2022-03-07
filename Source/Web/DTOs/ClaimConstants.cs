using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Common
{
    public class ClaimConstants
    {
        public const string NameClaimType = nameof(ClaimTypes.Name);
        public const string TeamRoleClaimType = "TeamRole";
        public const string TeamIdClaimType = "TeamId";
    }
}
