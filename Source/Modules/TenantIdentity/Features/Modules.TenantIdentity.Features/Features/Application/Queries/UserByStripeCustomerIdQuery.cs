using Modules.TenantIdentity.Domain;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
