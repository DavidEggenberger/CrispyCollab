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
    public class TeamAuthSchemeConfiguration : IEntityTypeConfiguration<TeamAuthScheme>
    {
        public void Configure(EntityTypeBuilder<TeamAuthScheme> builder)
        {
            builder.HasKey(x => new { x.AuthSchemeId, x.TeamId });
        }
    }
}
