using Common.Identity.ApplicationUser;
using Common.Identity.Team.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Team.DTOs
{
    public class TeamExtendedDTO
    {
        public List<ApplicationUserDTO> Members { get; set; }
        public SubscriptionStatusDTO SubscriptionStatus { get; set; }
        public SubscriptionPlanTypeDTO SubscriptionPlanType { get; set; }
    }
}
