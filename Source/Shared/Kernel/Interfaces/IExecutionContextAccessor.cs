using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;

namespace SharedKernel.Interfaces
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        Guid TenantId { get; }

        SubscriptionPlanType TenantPlan { get; }

        TenantRole TenantRole { get; }
    }
}
