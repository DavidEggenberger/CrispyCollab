using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TenantAggregate
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
