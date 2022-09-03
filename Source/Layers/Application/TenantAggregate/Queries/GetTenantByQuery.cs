using Application.ChannelAggregate.Commands;
using Domain.Aggregates.TenantAggregate;
using Infrastructure.CQRS.Command;
using Infrastructure.CQRS.Query;
using Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TenantAggregate.Queries
{
    public class GetTenantByQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
