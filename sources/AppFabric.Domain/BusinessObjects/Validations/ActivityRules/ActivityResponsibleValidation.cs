using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityResponsibleValidation : ValidationRule<Activity>
    {
        private readonly ValidationFailure _responsibleFailure;

        public ActivityResponsibleValidation()
        {
            _responsibleFailure = new ValidationFailure("CanHaveResponsible"
                , "Só é possível adicionar como responsável membros do projeto");
        }

        public override bool IsValid(Activity candidate)
        {
            if (candidate.Responsible.ProjectId.Value != Guid.Empty && candidate.ProjectId != candidate.Responsible.ProjectId)
            {
                candidate.AppendValidationResult(_responsibleFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}
