using Domain.Aggregates.TenantAggregate;

namespace Infrastructure.Interfaces
{
    public interface ITenantResolver
    {
        Guid ResolveTenantId();
        Task<Tenant> ResolveTenantAsync();
    }
}
