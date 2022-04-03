using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Kernel
{
    public class ValueObject
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid CreatedByUserId { get; set; }
    }
}
