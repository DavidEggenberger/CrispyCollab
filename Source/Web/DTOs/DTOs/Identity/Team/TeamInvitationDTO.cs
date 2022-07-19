using WebShared.Identity.UserTeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShared.Identity.Team
{
    public class TeamInvitationDTO
    {
        public string TeamName { get; set; }
        public TeamRoleDTO Role { get; set; }
    }
}
