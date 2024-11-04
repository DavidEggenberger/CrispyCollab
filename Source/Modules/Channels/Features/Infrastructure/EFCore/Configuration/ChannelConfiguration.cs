using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Channels.Features.DomainFeatures.Channels;

namespace Modules.Channels.Features.Infrastructure.EFCore.Configuration
{
    public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.Navigation(b => b.Messages)
                .HasField("messages")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
