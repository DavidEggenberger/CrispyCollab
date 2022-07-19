using Infrastructure.Identity;
using Infrastructure.Identity.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using System;

namespace WebServer.Services
{
    public class AuthenticationSchemeService : IAuthenticationSchemeService
    {
        private readonly IAuthenticationSchemeProvider schemeProvider;
        private readonly IOptionsMonitorCache<OpenIdConnectOptions> optionsCache;
        private readonly OpenIdConnectPostConfigureOptions postConfigureOptions;
        public AuthenticationSchemeService(IAuthenticationSchemeProvider schemeProvider, IOptionsMonitorCache<OpenIdConnectOptions> optionsCache, OpenIdConnectPostConfigureOptions postConfigureOptions)
        {
            this.schemeProvider = schemeProvider;
            this.optionsCache = optionsCache;
            this.postConfigureOptions = postConfigureOptions;
        }
        public void AddAuthenticationScheme(AuthScheme authScheme, OpenIdConnectOptions openIdConnectOptions = null)
        {
            var authenticationScheme = new AuthenticationScheme(authScheme.Name, authScheme.DisplayName, Type.GetType(authScheme.Handler));
            schemeProvider.AddScheme(authenticationScheme);
            if(openIdConnectOptions != null)
            {
                postConfigureOptions.PostConfigure(authenticationScheme.Name, openIdConnectOptions);
                optionsCache.TryAdd(authenticationScheme.Name, openIdConnectOptions);
            }
        }
        public void RemoveAuthenticationScheme(AuthScheme authScheme)
        {
            schemeProvider.RemoveScheme(authScheme.Name);
        }
    }
}
