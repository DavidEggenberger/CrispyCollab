using Modules.ChannelModule.Domain;
using Shared.Modules.Layers.Application.CQRS.Query;
using Modules.ChannelModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Modules.Layers.Infrastructure.CQRS.Query;

namespace Modules.ChannelModule.Layers.Application.Queries
{
    public class MessageByIdQuery : IQuery<Message>
    {
        public Guid Id { get; set; }
    }
}
