using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebServer.Pages.Identity.Shared
{
    public class _LogInSignUpSwitchModel : PageModel
    {
        public bool SignUpActive { get; set; }
        public string ReturnUrl { get; set; }
    }
}
