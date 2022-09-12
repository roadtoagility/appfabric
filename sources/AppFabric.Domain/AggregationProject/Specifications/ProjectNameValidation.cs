using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using DFlow.Domain.Validation;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectNameValidation : ValidationRule<Project>
    {
        private readonly Failure _nameEmptyFailure;

        public ProjectNameValidation()
        {
            _nameEmptyFailure = Failure.For("Project.Name", "O nome do projeto não pode estar vazio");
        }

        public override bool IsValid(Project candidate)
        {
            if (candidate.Name.ValidationStatus.IsValid == false)
            {
                candidate.AppendValidationResult(_nameEmptyFailure);
                return NotValid;
            }

            return Valid;
        }
    }
}