using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectOrderNumberValidation : ValidationRule<Project>
    {
        private ValidationFailure _osNullFailure;
        private ValidationFailure _osNotApprovedFailure;

        public ProjectOrderNumberValidation()
        {
            _osNullFailure = new ValidationFailure("Project.OrderNumber", "OS do projeto deve ser informada");
            _osNotApprovedFailure = new ValidationFailure("Project.OrderNumber", "A ordem de serviço precisa estar aprovada");
        }

        public override bool IsValid(Project candidate)
        {
            if (candidate.OrderNumber == null || string.IsNullOrEmpty(candidate.OrderNumber.Number))
            {
                candidate.AppendValidationResult(_osNullFailure);
                return NotValid;
            }
            else if (!candidate.OrderNumber.IsAproved)
            {
                candidate.AppendValidationResult(_osNotApprovedFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}
