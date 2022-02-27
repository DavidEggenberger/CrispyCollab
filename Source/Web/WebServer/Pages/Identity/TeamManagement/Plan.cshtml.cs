using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity;

namespace WebServer.Pages.Identity.TeamManagement
{
    public class ManagePlanModel : PageModel
    {
        private ApplicationUserManager applicationUserManager;
        private TeamManager teamManager;
        public ManagePlanModel(ApplicationUserManager applicationUserManager, TeamManager teamManager)
        {
            this.applicationUserManager = applicationUserManager;
            this.teamManager = teamManager;
        }
        public Team Team { get; set; }
        public async Task OnGet()
        {
            Team = await teamManager.FindByIdAsync(User.FindFirst("TeamId").Value);
        }
    }
}
