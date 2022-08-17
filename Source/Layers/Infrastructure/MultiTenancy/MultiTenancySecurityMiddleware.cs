using Common.Exstensions;
using Common.Kernel;
using Infrastructure.EFCore;
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

            //IEnumerable<EntityEntry> changeTrackerEntries;
            //if((changeTrackerEntries = context.RequestServices.GetRequiredService<ApplicationDbContext>().ChangeTracker.Entries()).Count() > 0)
            //{
            //    var ids = changeTrackerEntries.Where(e => e.Entity is ITenantIdentifiable)
            //        .Select(e => (e.Entity as ITenantIdentifiable).TenantId)
            //        .Distinct()
            //        .ToList();

            //    if(ids.Count == 0)
            //    {
            //        return;
            //    }

            //}
            //if (context.User.HasTenantIdClaim())
            //{
            //    var entries = applicationDbContext.ChangeTracker.Entries();
            //    if(entries.Count() == 0)
            //    {
            //        return;
            //    }

            //    Guid currentTenantId = context.User.GetTenantIdAsGuid();
            //    var ids = entries.Where(e => e.Entity is ITenantIdentifiable)
            //        .Select(e => (e.Entity as ITenantIdentifiable).TenantId)
            //        .Distinct()
            //        .ToList();


            //    if (ids.Count > 1)
            //    {
            //        throw new CrossTenantUpdateException(ids);
            //    }

            //    if (ids.First() != currentTenantId)
            //    {
            //        throw new CrossTenantUpdateException(ids);
            //    }
            //}
        }
    }
}
