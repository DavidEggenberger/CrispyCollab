using Infrastructure.Identity.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class ApplicationUserTeamManager
    {
        private IdentificationDbContext identificationDbContext;
        private ApplicationUserManager applicationUserManager;
        private TeamManager teamManager;
        public ApplicationUserTeamManager(ApplicationUserManager applicationUserManager, TeamManager teamManager, IdentificationDbContext identificationDbContext)
        {
            this.identificationDbContext = identificationDbContext;
            this.applicationUserManager = applicationUserManager;
            this.teamManager = teamManager;
        }
        public async Task<ApplicationUserTeam> GetCurrentTeamMembership(ClaimsPrincipal claimsPrincipal)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(claimsPrincipal);
            Team team = await teamManager.FindByClaimsPrincipalAsync(claimsPrincipal);
            ApplicationUserTeam applicationUserTeam;
            try
            {
                return await identificationDbContext.ApplicationUserTeams.SingleAsync(x => x.TeamId == team.Id && x.UserId == applicationUser.Id);
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException("");
            }
        }
    }
}
