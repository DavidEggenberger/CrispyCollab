using Common.Identity.ApplicationUser;
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
        public static async Task<TeamExtendedDTO> MapToTeamExtendedDTO(this Team team, IdentificationDbContext identificationDbContext)
        {
            await identificationDbContext.Entry(team).Collection(t => t.Members).Query().Include(x => x.User).LoadAsync();
            await identificationDbContext.Entry(team).Reference(t => t.Subscription).Query().Include(x => x.SubscriptionPlan).LoadAsync();
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
