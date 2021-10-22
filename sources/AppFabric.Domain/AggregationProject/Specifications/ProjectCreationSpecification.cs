using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectCreationSpecification : CompositeSpecification<Project>
    {
        public override bool IsSatisfiedBy(Project candidate)
        {
            throw new NotImplementedException();
        }
    }
}
