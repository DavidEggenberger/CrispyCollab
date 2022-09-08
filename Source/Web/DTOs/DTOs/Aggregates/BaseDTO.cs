using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShared.DTOs.Aggregates
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public Guid CreatedByUserId { get; set; }
    }
}
