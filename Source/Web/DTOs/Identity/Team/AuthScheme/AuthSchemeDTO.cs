using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Team.AuthScheme
{
    public class AuthSchemeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Handler { get; set; }
        public OpenIdOptionsDTO OpenIdOptions { get; set; }
    }
}
