using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivityCloseWithoutEffortSpecification : CompositeSpecification<Activity>
    {
        private readonly Failure _closedWithoutEffortFailure;

        public ActivityCloseWithoutEffortSpecification()
        {
            _closedWithoutEffortFailure = Failure.For("CanHaveResponsible"
                , "Não é possível fechar uma atividade com esforço pendente");
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.ActivityStatus == ActivityStatus.Closed() && candidate.Effort == Effort.Zero())
            {
                candidate.AppendValidationResult(_closedWithoutEffortFailure);
                return false;
            }

            return true;
        }
    }
}