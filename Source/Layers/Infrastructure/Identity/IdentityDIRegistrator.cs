using Infrastructure.Identity.Types.Overrides;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Constants;
using System.Security.Claims;
using WebServer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Infrastructure.Identity
{
    public static class IdentityDIRegistrator
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<OpenIdConnectPostConfigureOptions>();
            services.AddScoped<IUserResolver, UserResolver>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(0);
            });

            services.AddDbContext<IdentificationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityDbLocalConnectionString"), sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5);
                });
            });

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddLinkedIn(options =>
                {
                    options.ClientId = "test";
                    options.ClientSecret = "test";
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = "test";
                    options.ClientSecret = "test";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = configuration["SocialLogins:Google:ClientId"];
                    options.ClientSecret = configuration["SocialLogins:Google:ClientSecret"];
                    options.Scope.Add("profile");
                    options.Events.OnCreatingTicket = (context) =>
                    {
                        var picture = context.User.GetProperty("picture").GetString();
                        context.Identity.AddClaim(new Claim("picture", picture));
                        return Task.CompletedTask;
                    };
                });
            authenticationBuilder.AddExternalCookie().Configure(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "ExternalAuthenticationCookie";
            });
            authenticationBuilder.AddApplicationCookie().Configure(options =>
            {
                options.ExpireTimeSpan = new TimeSpan(6, 0, 0);
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "AuthenticationCookie";
                options.LoginPath = "/Identity/Login";
                options.LogoutPath = "/Identity/User/Logout";
                options.AccessDeniedPath = "/Identity/Forbidden";
                options.SlidingExpiration = true;
                options.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            });
            authenticationBuilder.AddTwoFactorUserIdCookie().Configure(options =>
            {
                options.Cookie.Name = "TwoFAUserIdCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            authenticationBuilder.AddTwoFactorRememberMeCookie().Configure(options =>
            {
                options.Cookie.Name = "TwoFARememberMeCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            var identityService = services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = true;
                options.Stores.MaxLengthForKeys = 128;
                options.ClaimsIdentity.UserIdClaimType = ClaimConstants.UserIdClaimType;
                options.ClaimsIdentity.UserNameClaimType = ClaimConstants.UserNameClaimType;
            })
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory<ApplicationUser>>()
                .AddUserManager<ApplicationUserManager>()
                .AddEntityFrameworkStores<IdentificationDbContext>()
                .AddSignInManager();

            return services;
        }
    }
}
