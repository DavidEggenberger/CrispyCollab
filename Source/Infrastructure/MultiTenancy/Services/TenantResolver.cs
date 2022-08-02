using Domain.Aggregates.TenantAggregate;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebServer.Services
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TenantResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Tenant> ResolveTenantAsync()
        {
            return default;
        }

        public Guid ResolveTenantId()
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
