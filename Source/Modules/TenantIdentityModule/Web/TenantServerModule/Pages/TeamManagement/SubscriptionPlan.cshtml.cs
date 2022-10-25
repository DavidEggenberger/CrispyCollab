using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace WebServer.Pages.Identity.TeamManagement
{
    //public class ManagePlanModel : PageModel
    //{
    //    private ApplicationUserManager applicationUserManager;
    //    private TeamManager teamManager;
    //    private SubscriptionPlanManager subscriptionPlanManager;
    //    public ManagePlanModel(ApplicationUserManager applicationUserManager, TeamManager teamManager, SubscriptionPlanManager subscriptionPlanManager)
    //    {
    //        this.applicationUserManager = applicationUserManager;
    //        this.teamManager = teamManager;
    //        this.subscriptionPlanManager = subscriptionPlanManager;
    //    }
    //    public List<SubscriptionPlan> SubscriptionPlans { get; set; }
    //    public Team Team { get; set; }
    //    public async Task OnGet()
    //    {
    //        Team = await teamManager.FindByClaimsPrincipalAsync(User);
    //        SubscriptionPlans = await subscriptionPlanManager.LoadAllSubscriptionPlans();
    //    }
    //}
}
