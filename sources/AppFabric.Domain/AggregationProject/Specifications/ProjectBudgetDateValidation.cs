using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectBudgetDateValidation : ValidationRule<Project>
    {
        private readonly Failure _negativeBudgetFailure;

        public ProjectBudgetDateValidation()
        {
            _negativeBudgetFailure =
                Failure.For("Project.Budget", "Não é possível informar um budget com valor negativo");
        }

        public override bool IsValid(Project candidate)
        {
            if (candidate.Budget < Money.Zero())
            {
                candidate.AppendValidationResult(_negativeBudgetFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}