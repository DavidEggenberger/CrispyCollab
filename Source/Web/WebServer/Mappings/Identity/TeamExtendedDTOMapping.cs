using Common.DTOs.Identity.Team;
using Common.Identity.ApplicationUser;
using Common.Identity.DTOs.TeamDTOs;
using Common.Identity.Team.DTOs;
using Common.Identity.Team.DTOs.Enums;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Mappings.Identity
{
    public static class TeamExtendedDTOMapping
    {
        public static async Task<TeamExtendedDTO> MapToTeamExtendedDTO(this Team team)
        {
            return new TeamExtendedDTO
            {
                Name = team.Name,
                IconUrl = "adsf",
                Id = team.Id,
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
