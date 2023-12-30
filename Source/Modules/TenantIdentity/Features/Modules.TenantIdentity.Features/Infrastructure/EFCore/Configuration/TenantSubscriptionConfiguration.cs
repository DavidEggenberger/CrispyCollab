using Modules.TenantIdentity.Features.Aggregates.TenantAggregate;

namespace Modules.TenantIdentity.Features.EFCore.Configuration
{
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {

        }
    }
}
