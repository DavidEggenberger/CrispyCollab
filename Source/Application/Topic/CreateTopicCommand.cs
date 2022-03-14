using Infrastructure.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Topic.CreateTopic
{
    public class CreateTopicCommand : ICommand
    {
    }
    public class CreateTopicCommandHandler : ICommandHandler<CreateTopicCommand, bool>
    {
        public Task<bool> HandleAsync(CreateTopicCommand query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
