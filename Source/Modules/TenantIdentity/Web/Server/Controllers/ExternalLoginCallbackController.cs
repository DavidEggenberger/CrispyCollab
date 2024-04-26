using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.SharedKernel.Exstensions;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;
using Shared.Kernel.Extensions.ClaimsPrincipal;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate.Application.Commands;
using Shared.Features.Server;

namespace Modules.TenantIdentity.Web.Server
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ExternalLoginCallbackController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ExternalLoginCallbackController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IServiceProvider services) : base(services)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (info is not null && user is null)
            {
                ApplicationUser _user = new ApplicationUser
                {
                    UserName = info.Principal.Identity.Name,
                    Email = info.Principal.GetClaimValue(ClaimConstants.EmailClaimType),
                    PictureUri = info.Principal.GetClaimValue(ClaimConstants.PictureClaimType)
                };

                var createUserCommand = new CreateNewUser { User = _user };
                await commandDispatcher.DispatchAsync(createUserCommand);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: false);
            return signInResult switch
            {
                Microsoft.AspNetCore.Identity.SignInResult { Succeeded: true } => LocalRedirect("/"),
                Microsoft.AspNetCore.Identity.SignInResult { RequiresTwoFactor: true } => RedirectToPage("/TwoFactorLogin", new { ReturnUrl = returnUrl }),
                _ => LocalRedirect("/")
            };
        }
    }
}
