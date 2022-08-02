using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.EFCore.Configuration
{
    //public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    //{
    //    //public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    //    //{
    //    //    builder.HasMany(x => x.CreatedTeams)
    //    //        .WithOne(x => x.Creator)
    //    //        .HasForeignKey(x => x.CreatorId)
    //    //        .OnDelete(DeleteBehavior.NoAction);

    //    //    builder.Navigation(b => b.Memberships)
    //    //        .HasField("memberships")
    //    //        .UsePropertyAccessMode(PropertyAccessMode.Field);
    //    //}
    //}
}
