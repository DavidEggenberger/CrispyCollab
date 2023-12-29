using Modules.TenantIdentity.Domain;
using System.Security.Claims;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class ClaimsForUserQuery : IQuery<IEnumerable<Claim>>
    {
        public ApplicationUser User { get; set; }
    }
}
