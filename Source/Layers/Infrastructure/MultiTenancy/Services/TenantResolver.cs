using Domain.Aggregates.TenantAggregate;
using Infrastructure.EFCore;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Common.Exstensions;

namespace WebServer.Services
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext applicationDbContext;
        public TenantResolver(IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.applicationDbContext = applicationDbContext;
        }

        public Task<Tenant> ResolveTenantAsync()
        {
            return applicationDbContext.Tenants.SingleAsync(t => t.Id == ResolveTenantId());
        }

        public Guid ResolveTenantId()
        {
            try
            {
                return httpContextAccessor.HttpContext.User.GetTenantIdAsGuid();
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }
    }
}
