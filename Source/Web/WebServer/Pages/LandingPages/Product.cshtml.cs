using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

namespace WebServer.Pages.LandingPages
{
    public class ProductModel : PageModel
    {
        public ProductModel(IDistributedCache cache)
        {
            cache.Set("asdf", Encoding.UTF8.GetBytes("david"));
        }
        public void OnGet()
        {
        }
    }
}
