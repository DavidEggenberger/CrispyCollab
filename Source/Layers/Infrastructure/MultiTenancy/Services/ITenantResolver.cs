using Domain.Aggregates.TenantAggregate;
using System.Runtime.CompilerServices;

namespace Infrastructure.Interfaces
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
