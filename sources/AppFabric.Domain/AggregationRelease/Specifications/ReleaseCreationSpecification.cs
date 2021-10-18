using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationRelease.Specifications
{
    public class ReleaseCreationSpecification : CompositeSpecification<Release>
    {
        public override bool IsSatisfiedBy(Release candidate)
        {
            //TODO: criar uma validação
            return true;
        }
    }
}
