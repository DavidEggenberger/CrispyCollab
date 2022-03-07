using Common.Identity.Team.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.ApplicationUser
{
    public class TeamUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUri { get; set; }
        public TeamRoleDTO Role { get; set; }
    }
}
