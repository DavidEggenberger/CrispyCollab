using Modules.TenantIdentity.Domain;
using Shared.Features.CQRS.Query;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
