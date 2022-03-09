using Common.Identity.Team.DTOs.Enums;
using Common.Identity.User;
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
        public List<TeamUserDTO> Members { get; set; }
        public SubscriptionStatusDTO SubscriptionStatus { get; set; }
        public SubscriptionPlanTypeDTO SubscriptionPlanType { get; set; }
    }
}
