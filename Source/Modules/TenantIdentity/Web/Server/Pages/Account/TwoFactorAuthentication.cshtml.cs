using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebServer.Pages.Identity.Account
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public TwoFactorAuthenticationModel(ApplicationUserManager applicationUserManager, SignInManager<ApplicationUser> signInManager)
        {
            this.applicationUserManager = applicationUserManager;
            this.signInManager = signInManager;
        }

        public ApplicationUser CurrentUser { get; set; }
        public async Task OnGetAsync()
        {
            CurrentUser = await applicationUserManager.GetUserAsync(HttpContext.User);
        }


    }
}
