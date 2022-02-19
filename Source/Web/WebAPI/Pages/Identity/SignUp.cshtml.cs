using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Pages.Identity
{
    public class SignUpModel : PageModel
    {
        public readonly SignInManager<ApplicationUser> signInManager;
        public readonly ApplicationUserManager applicationUserManager;

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        public SignUpModel(SignInManager<ApplicationUser> signInManager, ApplicationUserManager applicationUserManager)
        {
            this.signInManager = signInManager;
            this.applicationUserManager = applicationUserManager;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/");
            }
            else
            {
                return Page();
            }
        }

        public ActionResult OnPost([FromForm] string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
    }
}
