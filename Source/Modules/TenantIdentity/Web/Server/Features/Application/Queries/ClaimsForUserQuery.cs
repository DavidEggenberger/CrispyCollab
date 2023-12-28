using Modules.IdentityModule.Domain;
using Modules.TenantIdentity.Domain;
using Shared.Features.CQRS.Query;
using System.Security.Claims;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class ClaimsForUserQuery : IQuery<IEnumerable<Claim>>
    {
        public ApplicationUser User { get; set; }
    }
}
