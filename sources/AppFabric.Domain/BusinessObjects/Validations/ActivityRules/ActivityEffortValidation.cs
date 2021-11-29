using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityEffortValidation : ValidationRule<Activity>
    {
        private readonly ValidationFailure _effortFailure;

        public ActivityEffortValidation()
        {
            _effortFailure = new ValidationFailure("IsEffortLessOrEqualEightHours"
                , "Uma atividade não pode ter esforço maior do que 8 horas");
        }

        public override bool IsValid(Activity candidate)
        {
            if (candidate.Effort > Effort.MaxEffort())
            {
                candidate.AppendValidationResult(_effortFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}
