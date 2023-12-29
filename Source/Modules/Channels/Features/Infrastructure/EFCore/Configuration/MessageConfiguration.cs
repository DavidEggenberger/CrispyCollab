using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Channels.Features.ChannelAggregate;

namespace Modules.Channels.Features.Infrastructure.EFCore.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.OwnsMany(x => x.MakeMessageTopicVotes)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
