using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationRelease.Specifications
{
    public class ReleaseSpecification : CompositeSpecification<Release>
    {
        public override bool IsSatisfiedBy(Release candidate)
        {
            //TODO:
            //RuleFor(project => project.Id).SetValidator(new EntityIdValidator());
            throw new NotImplementedException();
        }
    }
}
