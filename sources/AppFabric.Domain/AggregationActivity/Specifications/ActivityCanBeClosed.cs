using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivityCanBeClosed : CompositeSpecification<Activity>
    {
        private readonly ValidationFailure _cannotCloseFailure;

        public ActivityCanBeClosed()
        {
            _cannotCloseFailure = new ValidationFailure("CanHaveResponsible"
                , "Não é possível fechar uma atividade com esforço pendente");
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.ActivityStatus.Equals(ActivityStatus.Closed()) == false &&
                candidate.Effort.Equals(Effort.Zero()))
                return true;

            candidate.AppendValidationResult(_cannotCloseFailure);
            return false;
        }
    }
}