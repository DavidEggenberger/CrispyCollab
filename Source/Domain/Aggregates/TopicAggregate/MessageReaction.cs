using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.MessagingAggregate
{
    public class MessageReaction : Entity
    {
        public Guid MyProperty { get; set; }
        public Guid UserId { get; set; }
    }
}
