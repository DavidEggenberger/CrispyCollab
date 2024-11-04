using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using System;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants
{
    public class TenantMembership : Entity
    {
        private TenantMembership() { }
        public TenantMembership(Guid userId, TenantRole role)
        {
            UserId = userId;
            Role = role;
        }
        public Guid UserId { get; set; }
        public Tenant Tenant { get; set; }
        public TenantRole Role { get; set; }
    }
}
