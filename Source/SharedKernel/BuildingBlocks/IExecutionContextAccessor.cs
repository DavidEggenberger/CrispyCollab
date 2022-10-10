using Common.DomainKernel.Tenant;

namespace Common.BuildingBlocks
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        Guid TenantId { get; }

        SubscriptionPlanType TenantPlan { get; }

        TenantRole TenantRole { get; }
    }
}
