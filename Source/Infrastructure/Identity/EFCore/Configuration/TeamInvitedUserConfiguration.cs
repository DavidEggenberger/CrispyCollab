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
    public class TeamInvitedUserConfiguration : IEntityTypeConfiguration<TeamInvitedUser>
    {
        public void Configure(EntityTypeBuilder<TeamInvitedUser> builder)
        {
            builder.HasKey(x => new { x.InvitedUserId, x.TeamId });
        }
    }
}
