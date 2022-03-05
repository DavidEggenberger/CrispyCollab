using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebServer.Pages.Identity.Account
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public IndexModel(
            ApplicationUserManager applicationUserManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.applicationUserManager = applicationUserManager;
            this.signInManager = signInManager;
        }

        public ApplicationUser CurrentUser { get; set; }
        public async Task OnGetAsync()
        {
            CurrentUser = await applicationUserManager.GetUserAsync(HttpContext.User);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string NewUserName { get; set; }

        public async Task<ActionResult> OnPostChangeUserName()
        {
            var user = await applicationUserManager.GetUserAsync(HttpContext.User);
            user.UserName = NewUserName;
            await applicationUserManager.UpdateAsync(user);
            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
