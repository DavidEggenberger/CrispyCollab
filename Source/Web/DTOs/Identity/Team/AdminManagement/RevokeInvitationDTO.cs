using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Team.AdminManagement
{
    public class RevokeInvitationDTO
    {
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
    }
}
