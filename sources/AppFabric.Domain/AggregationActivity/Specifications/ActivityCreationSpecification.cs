using System.Collections.Generic;
using System.Linq;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.BusinessObjects.Validations.GenericRules;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivityCreationSpecification : CompositeSpecification<Activity>
    {
        private readonly Effort _limitToCheck;
        public ActivityCreationSpecification(Effort limitToCheck)
        {
            _limitToCheck = limitToCheck;
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            return _limitToCheck >= candidate.Effort;
        }
    }
}