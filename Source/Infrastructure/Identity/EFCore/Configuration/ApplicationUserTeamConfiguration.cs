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
            builder.Navigation(x => x.Team).AutoInclude();
        }
    }
}
