using Common.Identity.ApplicationUser;
using Common.Identity.Team.DTOs;
using Common.Identity.Team.DTOs.Enums;
using Infrastructure.Identity;
using System.Linq;

namespace WebServer.DTOMappings.Identity
{
    public static class TeamExtendedDTOMapping
    {
        public static TeamExtendedDTO MapToTeamExtendedDTO(this Team team)
        {
            return new TeamExtendedDTO
            {
                SubscriptionPlanType = (SubscriptionPlanTypeDTO)team.Subscription.SubscriptionPlan.PlanType,
                Members = team.Members.Select(x => new TeamUserDTO 
                { 
                    Email = x.User.Email, 
                    Role = (TeamRoleDTO)x.Role, 
                    UserName = x.User.UserName 
                }).ToList()
            };
        }
    }
}
