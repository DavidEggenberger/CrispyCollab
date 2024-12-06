using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain;
using Modules.TenantIdentity.Shared.DTOs.Tenant;
using Shared.Features.Domain;
using System;
using Shared.Kernel.DomainKernel;

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
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public TenantRole Role { get; set; }

        public TenantMembershipDTO ToDTO()
        {
            return new TenantMembershipDTO()
            {
                UserId = UserId,
                TenantId = Tenant.Id,
                Role = Role
            };
        }
    }

    public class TenantMembershipEFConfiguration : IEntityTypeConfiguration<TenantMembership>
    {
        public void Configure(EntityTypeBuilder<TenantMembership> builder)
        {
            builder.ToTable("TenantMembership");
        }
    }
}
