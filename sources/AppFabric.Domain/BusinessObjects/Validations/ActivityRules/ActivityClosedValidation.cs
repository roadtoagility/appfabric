using FluentValidation.Results;
using System;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityClosedValidation : ValidationRule<Activity>
    {
        private readonly ValidationFailure _cannotCloseFailure;

        public ActivityClosedValidation()
        {
            _cannotCloseFailure = new ValidationFailure("CanHaveResponsible"
                , "Não é possível fechar uma atividade com esforço pendente");
        }

        public override bool IsValid(Activity candidate)
        {
            if (candidate.ActivityStatus != ActivityStatus.Closed() ||
                candidate.Effort > Effort.WithoutEffort())
            {
                return Valid;
            }

            candidate.AppendValidationResult(_cannotCloseFailure);
            return NotValid;
        }
    }
}
