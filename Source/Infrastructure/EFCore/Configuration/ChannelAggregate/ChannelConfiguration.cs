using Microsoft.EntityFrameworkCore;
using System;
using Domain.Aggregates.ChannelAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Configuration.ChannelAggregate
{
    public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            throw new NotImplementedException();
        }
    }
}
