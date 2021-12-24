using FluentValidation.Results;
using System;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityClosedSpecification : CompositeSpecification<Activity>
    {
        private readonly ValidationFailure _cannotCloseFailure;

        public ActivityClosedSpecification()
        {
            _cannotCloseFailure = new ValidationFailure("CanHaveResponsible"
                , "Não é possível fechar uma atividade com esforço pendente");
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.ActivityStatus != ActivityStatus.Closed() ||
                candidate.Effort > Effort.WithoutEffort())
            {
                return true;
            }

            candidate.AppendValidationResult(_cannotCloseFailure);
            return false;
        }
    }
}
