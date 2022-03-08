using Domain.SharedKernel;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public IReadOnlyCollection<ApplicationUserTeam> Members => members.AsReadOnly();
        private List<ApplicationUserTeam> members = new List<ApplicationUserTeam>();
        public void AddMember(ApplicationUser applicationUser, TeamRole teamRole)
        {
            members.Add(new ApplicationUserTeam
            {
                Role = teamRole,
                User = applicationUser,
                
            });
        }
    }
}
