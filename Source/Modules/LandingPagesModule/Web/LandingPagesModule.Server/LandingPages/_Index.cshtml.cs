using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebServer.Pages.LandingPages
{
    public class IndexModel : PageModel
    {
        public int MyProperty { get; set; }
        public IndexModel()
        {
            
        }
        public void OnGet()
        {
        }
    }
}
