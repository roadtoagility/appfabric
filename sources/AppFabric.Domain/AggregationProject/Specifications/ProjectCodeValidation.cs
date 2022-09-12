using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations;
using DFlow.Domain.Validation;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectCodeValidation : ValidationRule<Project>
    {
        public override bool IsValid(Project candidate)
        {
            if (candidate.Code.ValidationStatus.IsValid == false)
            {
                candidate.AppendValidationResult(Failure
                    .For("Project.Code", "O código do projeto não pode estar vazio"));
                return NotValid;
            }

            return Valid;
        }
    }
}