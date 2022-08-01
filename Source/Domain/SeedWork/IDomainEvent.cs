using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedKernel
{
    public interface IDomainEvent
    {
    }
    public interface IDomainEvent<IDomainEventResponse>
    {
    }
}
