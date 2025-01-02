namespace Modules.Channels.Public.DTOs.ChannelAggregate
{
    public class ChannelDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MessageDTO> Messages { get; set; }
    }
}
