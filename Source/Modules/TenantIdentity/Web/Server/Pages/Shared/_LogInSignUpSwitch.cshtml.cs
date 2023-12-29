using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebServer.Pages.Identity.Shared
{
    public class _LogInSignUpSwitchModel : PageModel
    {
        public bool SignUpActive { get; set; }
        public string ReturnUrl { get; set; }
    }
}
