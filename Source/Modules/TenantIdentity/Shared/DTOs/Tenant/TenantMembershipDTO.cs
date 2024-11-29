using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using System;

namespace Modules.TenantIdentity.Shared.DTOs.Tenant
{
    public class TenantMembershipDTO
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public TenantRole Role { get; set; }
    }
}
