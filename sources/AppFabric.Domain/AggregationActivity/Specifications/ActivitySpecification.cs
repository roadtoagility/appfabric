using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivitySpecification : CompositeSpecification<Activity>
    {
        public override bool IsSatisfiedBy(Activity candidate)
        {
            return candidate.IsValid;
        }
    }
}