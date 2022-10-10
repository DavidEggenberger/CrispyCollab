using WebShared.Identity.Team.DTOs.Enums;
using Modules.IdentityModule.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.IdentityModule.Web.DTOs
{
    public class ChangeRoleOfMemberDTO
    {
        public Guid UserId { get; set; }
        public TeamRoleDTO TargetRole { get; set; }
    }
}
