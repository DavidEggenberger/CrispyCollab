using Infrastructure.CQRS.Query;
using System.Security.Claims;

namespace Infrastructure.Identity.Queries
{
    public class ClaimsForUserQuery : IQuery<IEnumerable<Claim>>
    {
        public ApplicationUser User { get; set; }
    }
}
