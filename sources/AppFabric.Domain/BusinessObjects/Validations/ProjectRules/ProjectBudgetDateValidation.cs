using FluentValidation.Results;
using System;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectBudgetDateValidation : ValidationRule<Project>
    {
        private ValidationFailure _budgetNullFailure;
        private ValidationFailure _negativeBudgetFailure;

        public ProjectBudgetDateValidation()
        {
            _budgetNullFailure = new ValidationFailure("Project.Budget", "Budget do projeto deve ser informado");
            _negativeBudgetFailure = new ValidationFailure("Project.Budget", "Não é possível informar um budget com valor negativo");
        }

        public override bool IsValid(Project candidate)
        {
            if (candidate.Budget == null)
            {
                candidate.AppendValidationResult(_budgetNullFailure);
                return NotValid;
            }
            else if (candidate.Budget.Value < Decimal.Zero)
            {
                candidate.AppendValidationResult(_negativeBudgetFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}
