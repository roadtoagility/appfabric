using DFlow.Domain.Specifications;
using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityEffortSpecification : CompositeSpecification<Activity>
    {
        private readonly Failure _effortFailure;

        public ActivityEffortSpecification()
        {
            _effortFailure = Failure.For("IsEffortLessOrEqualEightHours"
                , "Uma atividade não pode ter esforço maior do que 8 horas");
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.Effort > Effort.MaxEffort())
            {
                candidate.AppendValidationResult(_effortFailure);
                return false;
            }

            return true;
        }
    }
}