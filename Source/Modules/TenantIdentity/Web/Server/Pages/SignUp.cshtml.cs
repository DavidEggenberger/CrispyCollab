using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.TenantIdentity.Features.DomainFeatures.Users;

namespace Web.Server.Pages.Identity
{
    public class SignUpModel : PageModel
    {
        public readonly SignInManager<ApplicationUser> signInManager;
        public readonly UserManager<ApplicationUser> applicationUserManager;

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        public SignUpModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> applicationUserManager)
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
            var redirectUrl = Url.Action("ExternalSignUpCallback", "AccountCallback", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
    }
}
