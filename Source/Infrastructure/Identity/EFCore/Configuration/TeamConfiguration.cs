using Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.EFCore.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasOne(x => x.Subscription)
                .WithOne(x => x.Team)
                .HasForeignKey<Subscription>(x => x.TeamId);

            builder.HasMany(x => x.SelectedByUsers)
                .WithOne(x => x.SelectedTeam)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.Navigation(b => b.Members)
                .HasField("members")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
