using System;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectStartDateValidation : ValidationRule<Project>
    {
        private readonly ValidationFailure _dateNullFailure;
        private readonly ValidationFailure _invalidDateFailure;

        public ProjectStartDateValidation()
        {
            _dateNullFailure = new ValidationFailure("Project.StartDate", "Data não pode ser nula");
            _invalidDateFailure = new ValidationFailure("Project.StartDate", "Data de inicio inválida");
        }

        public override bool IsValid(Project candidate)
        {
            if (candidate.StartDate == null)
            {
                candidate.AppendValidationResult(_dateNullFailure);
                return NotValid;
            }

            if (candidate.StartDate.Value >= DateTime.UnixEpoch)
            {
                candidate.AppendValidationResult(_invalidDateFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}