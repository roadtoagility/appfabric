using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectCodeValidation : ValidationRule<Project>
    {
        private ValidationFailure _codeEmptyFailure;
        public ProjectCodeValidation()
        {
            _codeEmptyFailure = new ValidationFailure("Project.Code", "O código do projeto não pode estar vazio");
        }

        public override bool IsValid(Project candidate)
        {
            if (string.IsNullOrEmpty(candidate.Code.Value))
            {
                candidate.AppendValidationResult(_codeEmptyFailure);
                return NOT_VALID;
            }
            return VALID;
        }
    }
}
