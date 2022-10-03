using Modules.ChannelModule.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Configuration.ChannelAggregate
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
