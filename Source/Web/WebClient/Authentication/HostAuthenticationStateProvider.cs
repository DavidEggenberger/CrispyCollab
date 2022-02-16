using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared;
using Shared.DTOs.Identity;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebClient.Authentication
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);
        private readonly NavigationManager navigationManager;
        private readonly HttpClient httpClient;

        private DateTimeOffset userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
        private ClaimsPrincipal cachedUser = new ClaimsPrincipal(new ClaimsIdentity());

        public HostAuthenticationStateProvider(NavigationManager navigationManager, HttpClient httpClient)
        {
            this.navigationManager = navigationManager;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(await GetUser());
        }

        private async ValueTask<ClaimsPrincipal> GetUser()
        {
            if (DateTime.Now < (userLastCheck + UserCacheRefreshInterval))
            {
                return cachedUser;
            }
            else
            {
                userLastCheck = DateTime.Now;
                return await FetchUser();
            }
        }

        private async Task<ClaimsPrincipal> FetchUser()
        {
            try
            {
                var response = await httpClient.GetAsync(EndpointConstants.UserClaimsPath);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bffUserInfoDTO = await response.Content.ReadFromJsonAsync<BFFUserInfoDTO>();

                    var identity = new ClaimsIdentity(
                        nameof(HostAuthenticationStateProvider),
                        bffUserInfoDTO.NameClaimType,
                        ClaimTypes.Role);

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
            var returnUrl = customReturnUrl != null ? navigationManager.ToAbsoluteUri(customReturnUrl).ToString() : null;
            var encodedReturnUrl = Uri.EscapeDataString(returnUrl ?? navigationManager.Uri);
            var logInUrl = navigationManager.ToAbsoluteUri($"{EndpointConstants.LoginPath}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void SignUp(string customReturnUrl = null)
        {
            var returnUrl = customReturnUrl != null ? navigationManager.ToAbsoluteUri(customReturnUrl).ToString() : null;
            var encodedReturnUrl = Uri.EscapeDataString(returnUrl ?? navigationManager.Uri);
            var logInUrl = navigationManager.ToAbsoluteUri($"{EndpointConstants.SignUp}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void SignOut()
        {
            navigationManager.NavigateTo(EndpointConstants.LogoutPath, true);
        }
    }
}
