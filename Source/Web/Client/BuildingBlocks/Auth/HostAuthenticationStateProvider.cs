using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using Modules.IdentityModule.Web.DTOs;
using Web.Client.BuildingBlocks.Authentication.Antiforgery;
using Shared.Kernel.BuildingBlocks.Auth.Constants;

namespace Web.Client.BuildingBlocks.Authentication
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AntiforgeryTokenService antiforgeryTokenService;
        private readonly NavigationManager navigationManager;
        private readonly HttpClient httpClient;
        private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);
        private DateTimeOffset userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
        private ClaimsPrincipal cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
        public HostAuthenticationStateProvider(NavigationManager navigationManager, HttpClient httpClient, AntiforgeryTokenService antiforgeryTokenService)
        {
            this.navigationManager = navigationManager;
            this.httpClient = httpClient;
            this.antiforgeryTokenService = antiforgeryTokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(await GetUser());
        }

        private async ValueTask<ClaimsPrincipal> GetUser()
        {
            if (DateTime.Now < userLastCheck + UserCacheRefreshInterval)
            {
                return cachedUser;
            }
            else
            {
                cachedUser = await FetchUser();
                userLastCheck = DateTime.Now;
                return cachedUser;
            }
        }

        private async Task<ClaimsPrincipal> FetchUser()
        {
            try
            {
                var response = await httpClient.GetAsync(Shared.Kernel.Constants.Modules.EndpointConstants.TenantIdentity.UserClaimsPath);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bffUserInfoDTO = await response.Content.ReadFromJsonAsync<BFFUserInfoDTO>();

                    var identity = new ClaimsIdentity(
                        nameof(HostAuthenticationStateProvider),
                        ClaimConstants.UserNameClaimType,
                        ClaimConstants.UserRoleInTenantClaimType);

                    foreach (var claim in bffUserInfoDTO.Claims)
                    {
                        identity.AddClaim(new Claim(claim.Type, claim.Value.ToString()));
                    }

                    return new ClaimsPrincipal(identity);
                }
            }
            catch (Exception ex)
            {

            }

            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        public void SignIn(string customReturnUrl = null)
        {
            var encodedReturnUrl = Uri.EscapeDataString(customReturnUrl ?? navigationManager.Uri);
            var logInUrl = navigationManager.ToAbsoluteUri($"{Shared.Kernel.Constants.Modules.EndpointConstants.TenantIdentity.LoginPath}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void SignUp(string customReturnUrl = null)
        {
            var encodedReturnUrl = Uri.EscapeDataString(customReturnUrl ?? navigationManager.Uri);
            var logInUrl = navigationManager.ToAbsoluteUri($"{Shared.Kernel.Constants.Modules.EndpointConstants.TenantIdentity.SignUpPath}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void GoToManageAccount()
        {
            navigationManager.NavigateTo(Shared.Kernel.Constants.Modules.EndpointConstants.TenantIdentity.IdentityAccountPath, true);
        }
        public void SignOut()
        {
            navigationManager.NavigateTo(Shared.Kernel.Constants.Modules.EndpointConstants.TenantIdentity.LogoutPath, true);
        }
    }
}
