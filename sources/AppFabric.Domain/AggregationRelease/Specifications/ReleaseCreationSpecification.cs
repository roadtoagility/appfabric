using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

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