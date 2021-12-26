using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ProjectRules
{
    public class ProjectNameValidation : ValidationRule<Project>
    {
        private readonly ValidationFailure _nameEmptyFailure;

        public ProjectNameValidation()
        {
            _nameEmptyFailure = new ValidationFailure("Project.Name", "O nome do projeto não pode estar vazio");
        }

        public override bool IsValid(Project candidate)
        {
            if (string.IsNullOrEmpty(candidate.Name.Value))
            {
                candidate.AppendValidationResult(_nameEmptyFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}