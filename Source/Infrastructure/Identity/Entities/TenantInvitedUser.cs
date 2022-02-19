using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class TenantInvitedUser
    {
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public Guid InvitedUserId { get; set; }
        public InvitedUser User { get; set; }
        public TenantRoleType Role { get; set; }
    }
}
