﻿using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using System;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate
{
    public class TenantSubscription : Entity
    {
        public string StripeSubscriptionId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
    }
}
