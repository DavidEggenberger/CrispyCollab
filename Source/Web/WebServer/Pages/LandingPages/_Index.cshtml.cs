using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Modules.HostingInformation;

namespace WebServer.Pages.LandingPages
{
    public class IndexModel : PageModel
    {
        public int MyProperty { get; set; }
        public IndexModel(IServerInformationProvider s)
        {
            
        }
        public void OnGet()
        {
        }
    }
}
