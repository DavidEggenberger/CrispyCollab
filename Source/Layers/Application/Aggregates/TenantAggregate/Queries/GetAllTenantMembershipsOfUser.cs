using Domain.Aggregates.TenantAggregate;
using Infrastructure.CQRS.Query;

namespace Application.TenantAggregate.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
