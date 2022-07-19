using WebShared.Identity.Team.DTOs.Enums;
using WebShared.Identity.UserTeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShared.Identity.DTOs.TeamDTOs
{
    public class ChangeRoleOfMemberDTO
    {
        public Guid UserId { get; set; }
        public TeamRoleDTO TargetRole { get; set; }
    }
}
