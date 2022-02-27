using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class InvitedUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<TeamInvitedUser> InvitedTeams { get; set; }
    }
}
