using Domain.Aggregates.TenantAggregate;
using Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TenantAggregate.Queries
{
    public class GetTenantByIdQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
