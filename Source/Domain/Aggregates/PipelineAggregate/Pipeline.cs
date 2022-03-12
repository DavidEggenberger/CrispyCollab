using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.PipelineAggregate
{
    [AggregateRoot]
    public class Pipeline : Entity
    {
        public List<PipelineStep> PipelineSteps { get; set; }
    }
}
