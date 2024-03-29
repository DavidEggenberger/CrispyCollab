using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;

namespace WebServer.Pages.Identity.Account
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> applicationUserManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public TwoFactorAuthenticationModel(UserManager<ApplicationUser> applicationUserManager, SignInManager<ApplicationUser> signInManager)
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
