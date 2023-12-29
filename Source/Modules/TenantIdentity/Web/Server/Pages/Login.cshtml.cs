using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebServer.Pages.Identity
{
    public class LoginModel : PageModel
    {
        public readonly SignInManager<ApplicationUser> SignInManager;

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            SignInManager = signInManager;
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

        public ActionResult OnPostExternalLogin([FromForm] string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "AccountCallback", new { returnUrl });
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
    }
}
