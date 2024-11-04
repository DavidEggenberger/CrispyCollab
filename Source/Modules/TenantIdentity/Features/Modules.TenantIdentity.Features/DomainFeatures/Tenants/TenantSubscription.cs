using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using System;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants
{
    public class TenantSubscription : Entity
    {
        public string StripeSubscriptionId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
    }

    public class SubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {

        }
    }
}
