using Common.Identity.Team.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.DTOs.TeamDTOs
{
    public class ChangeRoleOfTeamMemberDTO
    {
        public Guid UserId { get; set; }
        public TeamRoleDTO TargetRole { get; set; }
    }
}
