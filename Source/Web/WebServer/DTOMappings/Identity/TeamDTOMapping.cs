using Common.DTOs.Identity.Team;
using Infrastructure.Identity;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.DTOMappings.Identity
{
    public static class TeamDTOMapping
    {
        public static TeamDTO MapToTeamDTO(this Team team)
        {
            return new TeamDTO
            {
                Id = team.Id,
                Name = team.NameIdentitifer
            };
        }

        public static List<TeamDTO> MapToListTeamDTO(this List<ApplicationUserTeam> applicationUserTeams)
        {
            return applicationUserTeams.Select(x =>
            new TeamDTO
            {
                Name = x.Team.NameIdentitifer,
                Id = x.TeamId,
                IconUrl = "https://icon"
            }).ToList();
        }
    }
}
