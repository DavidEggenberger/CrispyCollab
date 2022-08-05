using Common.Exstensions;
using Common.Kernel;
using Infrastructure.EFCore;
using Microsoft.AspNetCore.Http;
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
            string tenantIdClaimValue;
            if((tenantIdClaimValue = context.User.GetTenantIdAsString()) != null)
            {
                await requestDelegate(context);
                ApplicationDbContext applicationDbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
                Guid currentTenantId = context.User.GetTenantIdAsGuid();

                var ids = applicationDbContext.ChangeTracker.Entries()
                    .Where(e => e.Entity is IIdentifiable)
                    .Select(e => (e.Entity as IIdentifiable).TenantId)
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

                if (ids.First() != currentTenantId)
                {
                    throw new CrossTenantUpdateException(ids);
                }

            }
            else
            {
                await requestDelegate(context);
            }
        }
    }
}
