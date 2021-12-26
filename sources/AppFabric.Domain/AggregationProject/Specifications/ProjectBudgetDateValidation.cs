using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectBudgetDateValidation : ValidationRule<Project>
    {
        private readonly ValidationFailure _budgetNullFailure;
        private readonly ValidationFailure _negativeBudgetFailure;

        public ProjectBudgetDateValidation()
        {
            _budgetNullFailure = new ValidationFailure("Project.Budget", "Budget do projeto deve ser informado");
            _negativeBudgetFailure =
                new ValidationFailure("Project.Budget", "Não é possível informar um budget com valor negativo");
        }

        public override bool IsValid(Project candidate)
        {
            if (candidate.Budget == null)
            {
                candidate.AppendValidationResult(_budgetNullFailure);
                return NotValid;
            }

            if (candidate.Budget.Value < decimal.Zero)
            {
                candidate.AppendValidationResult(_negativeBudgetFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}