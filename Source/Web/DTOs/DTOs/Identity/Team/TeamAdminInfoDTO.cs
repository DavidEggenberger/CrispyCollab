using WebShared.Identity.Subscription;
using WebShared.Identity.Team;
using WebShared.Identity.Team.AdminManagement;
using WebShared.Identity.Team.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShared.Identity.DTOs.TeamDTOs
{
    public class TeamAdminInfoDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public TeamMetricsDTO Metrics { get; set; }
        public List<MemberDTO> Members { get; set; }
        public List<AdminNotificationDTO> AdminNotifications { get; set; }
        public SubscriptionDTO Subscription { get; set; }
    }
}
