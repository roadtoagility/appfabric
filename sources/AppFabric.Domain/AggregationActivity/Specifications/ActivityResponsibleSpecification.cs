using System;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivityResponsibleSpecification : CompositeSpecification<Activity>
    {
        private readonly ValidationFailure _responsibleFailure;

        public ActivityResponsibleSpecification()
        {
            _responsibleFailure = new ValidationFailure("CanHaveResponsible"
                , "Só é possível adicionar como responsável membros do projeto");
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.ProjectId.ValidationStatus.IsValid &&
                candidate.ProjectId.Equals(candidate.Responsible.ProjectId) == false)
            {
                candidate.AppendValidationResult(_responsibleFailure);
                return false;
            }

            return true;
        }
    }
}