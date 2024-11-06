using Modules.TenantIdentity.IntegrationEvents;
using Shared.Features.Messaging.IntegrationEvent;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers.Aplication.IntegrationEventHandlers
{
    public class UserUpdatedIntegrationEventHandler : IIntegrationEventHandler<UserEmailUpdatedIntegrationEvent>
    {
        public async Task HandleAsync(UserEmailUpdatedIntegrationEvent userEmailUpdatedIntegrationEvent, CancellationToken cancellationToken)
        {

        }
    }
}
