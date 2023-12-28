using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Domain
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
