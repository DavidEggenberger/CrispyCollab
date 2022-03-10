using Common.Identity.DTOs.TeamDTOs;
using Common.Identity.Team.DTOs.Enums;
using Common.Identity.User;
using Common.Identity.UserTeam;
using Infrastructure.Identity;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.Mappings
{
    public static class TeamMappings
    {
        public static TeamDTO MapToTeamDTO(this Team team)
        {
            return new TeamDTO
            {
                Id = team.Id,
                Name = team.Name
            };
        }
        public static TeamAdminInfoDTO MapToTeamInfoAdminDTO(this Team team)
        {
            return new TeamAdminInfoDTO
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
