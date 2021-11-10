using FluentValidation.Results;
using System;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityClosedValidation : ValidationRule<Activity>
    {
        private ValidationFailure _cannotCloseFailure;

        public ActivityClosedValidation()
        {
            _cannotCloseFailure = new ValidationFailure("CanHaveResponsible", "Não é possível fechar uma atividade com esforço pendente");
        }

        public override bool IsValid(Activity candidate)
        {
            if (candidate.ActivityStatus.Value == (int)ActivityStatus.Status.Closed && candidate.Effort.Value > 0)
            {
                candidate.AppendValidationResult(_cannotCloseFailure);
                return NOT_VALID;
            }
                
            return VALID;
        }
    }
}
