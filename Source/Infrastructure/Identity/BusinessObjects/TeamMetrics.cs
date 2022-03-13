using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.BusinessObjects
{
    public class TeamMetrics
    {
        public int TotalUserCount { get; set; }
        public int JoinedUserCount { get; set; }
        public int InvitedUserCount { get; set; }
    }
}
