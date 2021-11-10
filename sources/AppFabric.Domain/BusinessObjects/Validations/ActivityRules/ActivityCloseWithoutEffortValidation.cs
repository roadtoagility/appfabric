using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityCloseWithoutEffortValidation : ValidationRule<Activity>
    {
        private ValidationFailure _closedWithoutEffortFailure;

        public ActivityCloseWithoutEffortValidation()
        {
            _closedWithoutEffortFailure = new ValidationFailure("CanHaveResponsible", "Não é possível fechar uma atividade com esforço pendente");
        }

        public override bool IsValid(Activity candidate)
        {
            if (candidate.ActivityStatus == ActivityStatus.From(2) && candidate.Effort.Value > 0)
            {
                candidate.AppendValidationResult(_closedWithoutEffortFailure);
                return NOT_VALID;
            }

            return VALID;
        }
    }
}
