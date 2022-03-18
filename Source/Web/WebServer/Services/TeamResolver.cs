using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace WebServer.Services
{
    public class TeamResolver : ITeamResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TeamResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public Guid GetTeamId()
        {
            try
            {
                return new Guid(httpContextAccessor.HttpContext.User.FindFirst("TeamId").Value);
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }
    }
}
