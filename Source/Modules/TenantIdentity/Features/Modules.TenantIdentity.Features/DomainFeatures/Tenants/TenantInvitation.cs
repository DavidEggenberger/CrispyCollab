using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using System;
using System.Collections.Generic;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants
{
    public class TenantInvitation : ValueObject
    {
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }
        public string Email { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Role;
        }
    }
}
