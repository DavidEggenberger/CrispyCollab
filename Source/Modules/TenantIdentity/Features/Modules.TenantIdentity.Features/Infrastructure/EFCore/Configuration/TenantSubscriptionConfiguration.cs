using Modules.TenantIdentity.Domain;

namespace Modules.TenantIdentity.Features.EFCore.Configuration
{
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {

        }
    }
}
