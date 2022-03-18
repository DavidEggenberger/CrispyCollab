using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class OpenIdOptions
    {
        public Guid Id { get; set; }
        public Guid AuthSchemeId { get; set; }
        public AuthScheme AuthScheme { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string ResponseType { get; set; }
    }
}
