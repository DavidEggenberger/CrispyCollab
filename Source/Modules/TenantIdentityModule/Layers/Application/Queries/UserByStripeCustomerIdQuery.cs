using Modules.IdentityModule.Domain;
using Modules.TenantIdentityModule.Domain;
using Shared.Modules.Layers.Infrastructure.CQRS.Query;
using Shared.Modules.Layers.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class UserByStripeCustomerIdQuery : IQuery<ApplicationUser>
    {
        public string StripeCustomerId { get; set; }
    }
}
