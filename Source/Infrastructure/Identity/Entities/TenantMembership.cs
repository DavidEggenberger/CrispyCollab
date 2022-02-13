using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class TenantMembership
    {
        public string TenatId { get; set; }
        public string TenantDescription { get; set; }
        public TenantRoleType Role { get; set; }
    }
}
