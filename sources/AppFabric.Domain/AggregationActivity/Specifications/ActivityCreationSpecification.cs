using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.BusinessObjects.Validations.GenericRules;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivityCreationSpecification : CompositeSpecification<Activity>
    {
        List<ValidationRule<BaseEntity<EntityId>>> _rules;

        public ActivityCreationSpecification()
        {
            _rules = new List<ValidationRule<BaseEntity<EntityId>>>();
            _rules.Add(new IdentityValidation());
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            var isSatisfiedBy = _rules.All(rule => rule.IsValid(candidate));
            return isSatisfiedBy;
        }
    }
}
