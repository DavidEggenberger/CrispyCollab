using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;
using System.Text.Json;

namespace WebServer.Pages.Identity.Account
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> applicationUserManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public PersonalDataModel(
            UserManager<ApplicationUser> applicationUserManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.applicationUserManager = applicationUserManager;
            this.signInManager = signInManager;
        }

        public ApplicationUser CurrentUser { get; set; }
        public async Task OnGetAsync()
        {
            CurrentUser = await applicationUserManager.GetUserAsync(HttpContext.User);
        }

        public async Task<ActionResult> OnPostDownloadPersonalData()
        {
            var user = await applicationUserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{applicationUserManager.GetUserId(User)}'.");
            }

            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(IdentityUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var logins = await applicationUserManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
        }
    }
}
