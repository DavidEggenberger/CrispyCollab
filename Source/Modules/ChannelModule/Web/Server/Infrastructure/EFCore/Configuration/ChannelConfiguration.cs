using ChannelModule.Server.Features.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.ChannelModule.Infrastructure.EFCore.Configuration
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
