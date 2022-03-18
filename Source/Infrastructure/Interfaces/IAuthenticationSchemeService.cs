using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticationSchemeService
    {
        void AddAuthenticationScheme(AuthScheme authScheme, OpenIdConnectOptions openIdConnectOptions = null);
        void RemoveAuthenticationScheme(AuthScheme authScheme);
    }
}
