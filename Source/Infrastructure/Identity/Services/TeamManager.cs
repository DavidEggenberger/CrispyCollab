using Infrastructure.Identity;
using Infrastructure.Identity.Types.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Types.Enums;

namespace Infrastructure.Identity.Services
{
    public class TeamManager
    {
        private IdentificationDbContext identificationDbContext;
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        public TeamManager(IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IdentityOperationResult> InviteUserToRoleThroughEmail(Team Team, TeamRoleType role, string Email)
        {
            throw new Exception();
        }
        public async Task<IdentityOperationResult> InviteUserThroughEmail(Team Team, string Email)
        {
            throw new Exception();
        }
        public async Task<Team> FindByIdAsync(string Id)
        {
            return identificationDbContext.Teams.Single(x => x.Id == new Guid(Id));
        }
        public async Task<IdentityOperationResult> CreateNewTeamAsync(string name)
        {

            identificationDbContext.Teams.Add(new Team
            {
                NameIdentitifer = name,
                
            });
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersAsync(Team Team)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            return IdentityOperationResult<List<ApplicationUser>>.Success(Team.Members.Select(x => x.User).ToList());
        }
        public async Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersByRoleAsync(Team Team, TeamRoleType role)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            return IdentityOperationResult<List<ApplicationUser>>.Success(Team.Members.Where(x => x.Role == role).Select(x => x.User).ToList());
        }
        public async Task<IdentityOperationResult> UpdateTeamNameAsync(Team Team, string newName)
        {
            throw new Exception();
        }
        public async Task<bool> CheckIfNameIsValidForTeamAsync(string name)
        {
            if(!identificationDbContext.Teams.Any(x => x.NameIdentitifer == name))
            {
                return true;
            }
            return false;
        }
        public async Task<IdentityOperationResult> AddNewUserToTeamAsync(ApplicationUser user, Team Team)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            if (_Team == null)
            {
                return IdentityOperationResult.Fail("Invalid Team");
            }
            ApplicationUserTeam _TeamUser = _Team.Members.First(x => x.UserId == user.Id);
            if (_TeamUser != null)
            {
                return IdentityOperationResult.Fail("User is already member of the Team");
            }
            _Team.Members.Add(new ApplicationUserTeam
            {
                User = user
            });
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult> ChangeRoleOfUserInTeamAsync(ApplicationUser user, Team Team, TeamRoleType TeamRoleType)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            if (_Team == null)
            {
                return IdentityOperationResult.Fail("Invalid Team");
            }
            ApplicationUserTeam _TeamUser = _Team.Members.First(x => x.UserId == user.Id);
            if (_TeamUser == null)
            {
                return IdentityOperationResult.Fail("User doesnt exist in Team");
            }
            _TeamUser.Role = TeamRoleType;
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult> SetCurrentSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser, Team Team)
        {
            if (CheckTeamMembershipOfApplicationUser(applicationUser, Team))
            {
                applicationUser.Memberships.ForEach(x => x.Status = TeamStatus.NotSelected);
                applicationUser.Memberships.Where(x => x.TeamId == Team.Id).First().Status = TeamStatus.Selected;
                await identificationDbContext.SaveChangesAsync();
                await signInManager.RefreshSignInAsync(applicationUser);
                return IdentityOperationResult.Success();
            }
            return IdentityOperationResult.Fail("User is not a member of the Team");
        }
        public async Task<IdentityOperationResult<Team>> GetCurrentSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = identificationDbContext.Users.Include(x => x.Memberships).ThenInclude(x => x.Team).Where(x => x.Id == applicationUser.Id).FirstOrDefault();
            Team Team = _applicationUser.Memberships.Where(x => x.Status == TeamStatus.Selected).First().Team;
            if (Team != null)
            {
                return IdentityOperationResult<Team>.Success(Team);
            }
            return IdentityOperationResult<Team>.Fail("User is not a member of the Team");
        }
        public bool CheckTeamMembershipOfApplicationUser(ApplicationUser applicationUser, Team Team)
        {
            if (applicationUser.Memberships.Any(x => x.TeamId == Team.Id))
            {
                return true;
            }
            return false;
        }
        public IdentityOperationResult<TeamRoleType> GetTeamRoleOfApplicationUser(ApplicationUser applicationUser, Team Team)
        {
            if (CheckTeamMembershipOfApplicationUser(applicationUser, Team))
            {
                return IdentityOperationResult<TeamRoleType>.Success(applicationUser.Memberships.Single(x => x.TeamId == Team.Id).Role);
            }
            return IdentityOperationResult<TeamRoleType>.Fail("User is not a member of the Team");
        }
    }
}
