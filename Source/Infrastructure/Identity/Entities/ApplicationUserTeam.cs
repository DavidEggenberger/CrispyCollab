using Domain.SharedKernel.Attributes;
using Infrastructure.Identity.Types.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUserTeam 
    {
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public TeamRole Role { get; set; }
        public UserSelectionStatus SelectionStatus { get; set; }
    }
}
