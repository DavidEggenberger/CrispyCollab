using Common.Exstensions;
using Common.Kernel;
using Infrastructure.EFCore;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MultiTenancy
{
    public class MultiTenancySecurityMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        public MultiTenancySecurityMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await requestDelegate(context);

            IEnumerable<EntityEntry> changeTrackerEntries;
            if (context.User.HasTenantIdClaim() && (changeTrackerEntries = context.RequestServices.GetRequiredService<ApplicationDbContext>().ChangeTracker.Entries()).Count() > 0)
            {
                var ids = changeTrackerEntries.Where(e => e.Entity is ITenantIdentifiable)
                    .Select(e => (e.Entity as ITenantIdentifiable).TenantId)
                    .Distinct()
                    .ToList();

                if (ids.Count == 0)
                {
                    return;
                }

                if (ids.Count > 1)
                {
                    throw new CrossTenantUpdateException(ids);
                }

                if (ids.First() != context.RequestServices.GetRequiredService<ITenantResolver>().ResolveTenantId())
                {
                    throw new CrossTenantUpdateException(ids);
                }
            }
        }
    }
}
