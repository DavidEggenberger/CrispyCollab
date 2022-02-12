using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Command
{
    interface ICommandHandler<in TCommand, TCommandResult> where TCommand : ICommand
    {
        Task<TCommandResult> Handle(TCommand query, CancellationToken cancellation);
    }
}
