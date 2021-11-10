using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.BusinessObjects.Validations.ActivityRules;
using DFlow.Domain.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivitySpecification : CompositeSpecification<Activity>
    {
        List<ValidationRule<Activity>> _rules;
        public ActivitySpecification()
        {
            _rules = new List<ValidationRule<Activity>>();
            _rules.Add(new ActivityClosedValidation());
            _rules.Add(new ActivityCloseWithoutEffortValidation());
            _rules.Add(new ActivityEffortValidation());
            _rules.Add(new ActivityResponsibleValidation());
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            var isSatisfiedBy = _rules.All(rule => rule.IsValid(candidate));
            return isSatisfiedBy;
        }
    }
}
