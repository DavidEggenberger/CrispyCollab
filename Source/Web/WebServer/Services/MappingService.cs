using Microsoft.AspNetCore.Authorization;

namespace WebServer.Services
{
    public class MappingService
    {
        private IAuthorizationService authorizationService;
        public MappingService(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        public static TeamDTO MapToTeamDTO(Team team)
        {
            return new TeamDTO
            {
                Id = team.Id,
                Name = team.Name
            };
        }

        public static List<TeamDTO> MapToListTeamDTO(List<ApplicationUserTeam> applicationUserTeams)
        {
            return applicationUserTeams.Select(x =>
            new TeamDTO
            {
                Name = x.Team.Name,
                Id = x.TeamId,
                IconUrl = "https://icon"
            }).ToList();
        }

        public static TeamAdminInfoDTO MapToTeamInfoAdminDTO(Team team)
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
