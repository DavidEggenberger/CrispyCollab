using Modules.TenantIdentityModule.Domain;
using Shared.Modules.Layers.Application.CQRS.Command;
using Shared.Modules.Layers.Application.CQRS.Query;
using Shared.Modules.Layers.Infrastructure.EFCore;
using Modules.TenantIdentityModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shared.Modules.Layers.Infrastructure.CQRS.Query;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class GetTenantByQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
