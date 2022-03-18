using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class TeamAuthScheme
    {
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public Guid AuthSchemeId { get; set; }
        public AuthScheme AuthScheme { get; set; }
    }
}
