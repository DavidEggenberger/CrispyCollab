using Domain.Kernel;
using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate
{
    public class Reaction : Entity
    {
        public bool Yes { get; set; }
    }
}
