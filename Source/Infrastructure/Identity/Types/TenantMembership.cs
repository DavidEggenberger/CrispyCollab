using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Types
{
    public class TenantMembership
    {
        public string Tenant { get; set; }
        public TenantRoleType Role { get; set; }
    }
}
