using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.PipelineAggregate
{
    public class PipelineStep
    {
        public Guid PipelineId { get; set; }
        public Pipeline Pipeline { get; set; }
    }
}
