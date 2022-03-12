using Common.Identity.Subscription;
using Common.Identity.Team;
using Common.Identity.Team.AdminManagement;
using Common.Identity.Team.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.DTOs.TeamDTOs
{
    public class TeamAdminInfoDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public List<MemberDTO> Members { get; set; }
        public List<AdminNotificationDTO> AdminNotifications { get; set; }
        public SubscriptionDTO Subscription { get; set; }
    }
}
