using Common.Identity.UserTeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Team
{
    public class MemberDTO
    {
        public TeamRoleDTO Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}
