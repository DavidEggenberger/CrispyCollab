using System.Security.Claims;
using System.Linq;
using Common.Identity.Team.DTOs.Enums;
using System;
using Common.Identity.UserTeam;
using Common;

namespace WebWasmClient.Authentication
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return new Guid(claimsPrincipal.FindFirst(ClaimTypes.Sid).Value);
        }
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "TeamRole")?.Value == TeamRoleDTO.Admin.ToString();
        }
        public static bool HasTeamClaims(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(x => x.Type == "TeamId") && claimsPrincipal.HasClaim(x => x.Type == "TeamName");
        }
        public static string GetTeamName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("TeamName")?.Value;
        }
        public static Guid? GetTeamId(this ClaimsPrincipal claimsPrincipal)
        {
            string teamId = claimsPrincipal.FindFirst("TeamId")?.Value;
            return teamId == null ? null : new Guid(teamId);
        }
        public static string GetAntiforgeryToken(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("AntiforgeryToken").Value;
        }
    }
}
