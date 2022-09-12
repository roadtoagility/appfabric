using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectDetailsCanBeUpdated : CompositeSpecification<Project>
    {
        private readonly Failure _codeEmptyFailure;

        public ProjectDetailsCanBeUpdated()
        {
            _codeEmptyFailure = Failure.For("Project.Code",
                "O código do projeto não pode estar vazio");
        }

        public override bool IsSatisfiedBy(Project candidate)
        {
            if (!candidate.Code.Equals(ProjectCode.Empty()) &&
                candidate.Name.ValidationStatus.IsValid &&
                candidate.Budget.ValidationStatus.IsValid &&
                candidate.Owner.ValidationStatus.IsValid &&
                candidate.Status.ValidationStatus.IsValid &&
                candidate.OrderNumber.ValidationStatus.IsValid)
            {
                candidate.AppendValidationResult(_codeEmptyFailure);
                return false;
            }

            return true;
        }
    }
}