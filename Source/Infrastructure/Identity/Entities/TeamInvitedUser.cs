using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class TeamInvitedUser
    {
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public Guid InvitedUserId { get; set; }
        public InvitedUser User { get; set; }
        public TeamRoleType Role { get; set; }
    }
}
