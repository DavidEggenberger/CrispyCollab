using Infrastructure.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TopicAggregate.Commands
{
    public class CreateTopicCommand : ICommand
    {
    }
    public class CreateTopicCommandHandler : ICommandHandler<CreateTopicCommand>
    {
        public Task HandleAsync(CreateTopicCommand query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
