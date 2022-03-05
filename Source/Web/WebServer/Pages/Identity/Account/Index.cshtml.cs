using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebServer.Pages.Identity.Account
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            signInManager = signInManager;
        }
    }
}
