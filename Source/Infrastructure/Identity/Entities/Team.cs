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
        public byte[] IconData { get; set; }
        public string NameIdentitifer { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public List<ApplicationUserTeam> Members { get; set; }   
    }
}
