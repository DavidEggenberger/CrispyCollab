using AutoMapper;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Infrastructure.Identity.Services
{
    public class AuthSchemeManager
    {
        private readonly IAuthenticationSchemeService authenticationSchemeService;
        private readonly IMapper mapper;
        private readonly IdentificationDbContext identificationDbContext;
        public AuthSchemeManager(IAuthenticationSchemeService authenticationSchemeService, IMapper mapper, IdentificationDbContext identificationDbContext)
        {
            this.authenticationSchemeService = authenticationSchemeService;
            this.mapper = mapper;
            this.identificationDbContext = identificationDbContext;
        }
        public async Task RemoveAuthenticationScheme(AuthScheme authScheme)
        {
            authenticationSchemeService.RemoveAuthenticationScheme(authScheme);
        }
        public async Task AddAuthenticationScheme(AuthScheme authScheme)
        {
            if (authScheme.OpenIdOptions != null)
            {
                var openIdConnectOptions = mapper.Map<OpenIdConnectOptions>(authScheme.OpenIdOptions);
                authenticationSchemeService.AddAuthenticationScheme(authScheme, openIdConnectOptions);
            }
            else
            {
                authenticationSchemeService.AddAuthenticationScheme(authScheme);
            }
        }
        
    }
}
