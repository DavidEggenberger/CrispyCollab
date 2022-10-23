using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShared.Identity.Team.AdminManagement
{
    public class TeamMetricsDTO
    {
        public int TotalUserCount { get; set; }
        public int JoinedUserCount { get; set; }
        public int InvitedUserCount { get; set; }
    }
}
