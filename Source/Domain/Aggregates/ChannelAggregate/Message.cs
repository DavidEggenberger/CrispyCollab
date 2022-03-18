using Domain.Kernel;
using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.MessagingAggregate
{
    public class Message : Entity, ICreatedByUser
    {
        public Guid CreatedByUserId { get; set; }
        public DateTime TimeSent { get; set; }
        public string Text { get; set; }
    }
}
