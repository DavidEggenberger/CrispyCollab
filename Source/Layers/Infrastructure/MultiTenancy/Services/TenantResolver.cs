using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Common.Exstensions;

namespace WebServer.Services
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TenantResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool CheckIfRequestHasTenant()
        {
            return httpContextAccessor.HttpContext.User.HasTenantIdClaim();
        }

        public Guid ResolveTenantId()
        {
            return httpContextAccessor.HttpContext.User.GetTenantIdAsGuid();
        }
    }
}
