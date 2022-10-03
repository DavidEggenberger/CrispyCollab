using Modules.TenantModule.Domain;
using Infrastructure.CQRS.Query;
using Modules.TenantModule.Domain;

namespace Application.TenantAggregate.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
