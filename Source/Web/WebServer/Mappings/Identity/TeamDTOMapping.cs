using Common.DTOs.Identity.Team;
using Infrastructure.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Mappings.Identity
{
    public static class TeamDTOMapping
    {
        public static async Task<TeamDTO> MapToTeamDTO(this Team team)
        {
            return new TeamDTO
            {
                Id = team.Id,
                Name = team.Name
            };
        }

        public static async Task<List<TeamDTO>> MapToListTeamDTO(this List<ApplicationUserTeam> applicationUserTeams)
        {
            return applicationUserTeams.Select(x =>
            new TeamDTO
            {
                Name = x.Team.Name,
                Id = x.TeamId,
                IconUrl = "https://icon"
            }).ToList();
        }
    }
}
