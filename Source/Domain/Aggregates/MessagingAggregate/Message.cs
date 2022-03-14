using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.MessagingAggregate
{
    [AggregateRoot]
    public class Message : Entity
    {
        public Guid TopicId { get; set; }
        public Guid ApproachId { get; set; }
        public Guid UserId { get; set; }
        public MessageType Type { get; set; }
        public List<Message> AnswerMessages { get; set; }
        public List<MessageReaction> Reactions { get; set; }
    }
}
