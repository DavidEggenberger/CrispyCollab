using Modules.TenantIdentity.Features.Aggregates.UserAggregate;

namespace Modules.TenantIdentity.Features.Aggregates.UserAggregate.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
