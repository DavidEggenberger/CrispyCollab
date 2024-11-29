using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.IntegrationEvents;
using Shared.Features.Messaging.IntegrationEvent;
using Shared.Features.Server;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.EventHandlers
{
    public class TenantSubscriptionUpdatedIntegrationEventHandler : ServerExecutionBase<TenantIdentityModule>, IIntegrationEventHandler<TenantSubscriptionPlanUpdatedIntegrationEvent>
    {
        public TenantSubscriptionUpdatedIntegrationEventHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task HandleAsync(TenantSubscriptionPlanUpdatedIntegrationEvent integrationEvent, CancellationToken cancellation)
        {
            var tenant = await module.TenantIdentityDbContext.Tenants.FirstAsync(tenant => tenant.Id == integrationEvent.TenantId);
            tenant.SubscriptionPlanType = integrationEvent.SubscriptionPlanType;

            await module.TenantIdentityDbContext.SaveChangesAsync();
        }
    }
}
