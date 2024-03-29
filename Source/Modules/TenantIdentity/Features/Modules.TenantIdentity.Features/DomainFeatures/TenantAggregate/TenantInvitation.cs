using Shared.Features.Domain;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using System;
using System.Collections.Generic;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate
{
    public class TenantInvitation : ValueObject
    {
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Role;
        }
    }
}
