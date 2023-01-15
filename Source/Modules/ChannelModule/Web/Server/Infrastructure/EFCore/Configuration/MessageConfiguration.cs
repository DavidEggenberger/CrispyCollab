using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChannelModule.Server.Features.Domain;

namespace Modules.ChannelModule.Infrastructure.EFCore.Configuration
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
