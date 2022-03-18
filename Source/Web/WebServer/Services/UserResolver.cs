using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace WebServer.Services
{
    public class UserResolver : IUserResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public Guid GetIdOfLoggedInUser()
        {
            return new Guid(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
        }
    }
}
