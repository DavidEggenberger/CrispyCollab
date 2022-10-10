using Modules.ChannelModule.Domain;
using Infrastructure.CQRS.Query;
using Modules.ChannelModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ChannelAggregate.Queries
{
    public class MessageByIdQuery : IQuery<Message>
    {
        public Guid Id { get; set; }
    }
}
