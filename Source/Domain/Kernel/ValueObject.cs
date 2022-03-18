using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Kernel
{
    public record ValueObject
    {
        public Guid CreatedByUserId { get; set; }
    }
}
