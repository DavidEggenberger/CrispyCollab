using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Server.Pages.Identity.Shared
{
    public class _LogInSignUpSwitchModel : PageModel
    {
        public bool SignUpActive { get; set; }
        public string ReturnUrl { get; set; }
    }
}
