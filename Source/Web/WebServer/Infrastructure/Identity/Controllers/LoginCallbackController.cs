﻿using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Stripe;

namespace WebServer.Infrastructure.Identity.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginCallbackController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager userManager;
        public LoginCallbackController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager userManager)
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
                    Email = info.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email).Value,
                    PictureUri = info.Principal.Claims.Where(c => c.Type == "picture").First().Value
                };

                var result = await userManager.CreateAsync(_user);

                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(_user, info);
                    await signInManager.SignInAsync(_user, isPersistent: false, info.LoginProvider);

                    var stripeCustomer = await new CustomerService().CreateAsync(new CustomerCreateOptions { Email = _user.Email });
                    _user.StripeCustomerId = stripeCustomer.Id;
                    await userManager.UpdateAsync(_user);

                    return LocalRedirect("/");
                }
                if (result.Succeeded is false)
                {
                    return RedirectToPage("/Error", new { IdentityErrors = result.Errors });
                }
            }

            string pictureUri = info.Principal.Claims.Where(c => c.Type == "picture").First().Value;
            if (user.PictureUri != pictureUri)
            {
                user.PictureUri = pictureUri;
                await userManager.UpdateAsync(user);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: false);
            return signInResult switch
            {
                Microsoft.AspNetCore.Identity.SignInResult { Succeeded: true } => LocalRedirect("/"),
                Microsoft.AspNetCore.Identity.SignInResult { RequiresTwoFactor: true } => RedirectToPage("/TwoFactorLogin", new { ReturnUrl = returnUrl }),
                _ => LocalRedirect("/")
            };
        }

        [HttpGet("ExternalSignUpCallback")]
        public async Task<IActionResult> ExternalSignUpCallback(string returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (info is not null && user is null)
            {
                ApplicationUser _user = new ApplicationUser
                {
                    UserName = info.Principal.Identity.Name,
                    Email = info.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email).Value,
                    PictureUri = info.Principal.Claims.Where(c => c.Type == "picture").First().Value
                };
                var result = await userManager.CreateAsync(_user);

                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(_user, info);
                    await signInManager.SignInAsync(_user, isPersistent: false, info.LoginProvider);

                    var stripeCustomer = await new CustomerService().CreateAsync(new CustomerCreateOptions { Email = _user.Email });
                    _user.StripeCustomerId = stripeCustomer.Id;
                    await userManager.UpdateAsync(_user);

                    return LocalRedirect("/");
                }
                if (result.Succeeded is false)
                {
                    return RedirectToPage("/Error", new { IdentityErrors = result.Errors });
                }
            }

            string pictureUri = info.Principal.Claims.Where(c => c.Type == "picture").First().Value;
            if (user.PictureUri != pictureUri)
            {
                user.PictureUri = pictureUri;
                await userManager.UpdateAsync(user);
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