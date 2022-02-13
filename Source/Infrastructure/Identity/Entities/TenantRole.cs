using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class TenantMembershipRole : IdentityRole<Guid>
    {

        public TenantRoleType Type { get; set; }
    }
}
