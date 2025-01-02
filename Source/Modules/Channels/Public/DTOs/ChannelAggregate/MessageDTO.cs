namespace Modules.Channels.Public.DTOs.ChannelAggregate
{
    public class MessageDTO
    {
        public Guid CreatedByUserId { get; set; }
        public DateTime TimeSent { get; set; }
        public string Text { get; set; }
        public bool HasDerivedTopic { get; set; }
        public string DerivedTopicId { get; set; }
    }
}
