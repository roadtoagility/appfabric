﻿using FluentValidation.Results;
using System.Linq;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectStatusValidation : ValidationRule<Project>
    {
        private ValidationFailure _statusNullFailure;
        private ValidationFailure _invalidStatusFailure;

        public ProjectStatusValidation()
        {
            _statusNullFailure = new ValidationFailure("Project.Status", "O nome do projeto não pode estar nulo");
            _invalidStatusFailure = new ValidationFailure("Project.Status", "Status inválido para o projeto");
        }
        public override bool IsValid(Project candidate)
        {
            if (candidate.Status == null)
            {
                candidate.AppendValidationResult(_statusNullFailure);
                return NOT_VALID;
            }
            else if (!Enumerable.Range(0, 2).Contains(candidate.Status.Value))
            {
                candidate.AppendValidationResult(_invalidStatusFailure);
                return NOT_VALID;
            }

            return VALID;
        }
    }
}