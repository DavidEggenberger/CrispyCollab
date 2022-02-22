using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Server.Pages.Identity
{
    [AllowAnonymous]
    public class LoginWith2FAModel : PageModel
    {
        private SignInManager<ApplicationUser> signInManager;
        public LoginWith2FAModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        [BindProperty]
        public string authenticatorCode { get; set; }

        public void OnGet(string errorMessage)
        {
            if (errorMessage != null)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
        }
        public async Task<ActionResult> OnPostAsync()
        {
            string formattedAuthenticatorCode = authenticatorCode.Replace(" ", string.Empty);
            var signInResult = await signInManager.TwoFactorAuthenticatorSignInAsync(formattedAuthenticatorCode, true, false);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
        }
    }
}
