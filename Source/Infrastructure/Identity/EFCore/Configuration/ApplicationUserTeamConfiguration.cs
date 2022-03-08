using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.EFCore.Configuration
{
    public class ApplicationUserTeamConfiguration : IEntityTypeConfiguration<ApplicationUserTeam>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserTeam> builder)
        {
            builder.HasKey(x => new { x.UserId, x.TeamId });
            
            builder.HasOne(x => x.Team)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(x => x.Team)
                .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Memberships)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(x => x.User)
                .IsRequired(false);
        }
    }
}
