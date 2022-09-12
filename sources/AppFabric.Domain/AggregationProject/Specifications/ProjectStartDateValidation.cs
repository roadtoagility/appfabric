using System;
using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectStartDateValidation : ValidationRule<Project>
    {
        private readonly Failure _dateNullFailure;
        private readonly Failure _invalidDateFailure;

        public ProjectStartDateValidation()
        {
            _dateNullFailure = Failure.For("Project.StartDate", "Data não pode ser nula");
            _invalidDateFailure = Failure.For("Project.StartDate", "Data de inicio inválida");
        }

        public override bool IsValid(Project candidate)
        {
            if (candidate.StartDate.Equals(null))
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