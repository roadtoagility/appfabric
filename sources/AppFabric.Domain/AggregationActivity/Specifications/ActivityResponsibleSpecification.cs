using System;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivityResponsibleSpecification : CompositeSpecification<Activity>
    {
        private readonly Failure _responsibleFailure;

        public ActivityResponsibleSpecification()
        {
            _responsibleFailure = Failure.For("CanHaveResponsible"
                , "Só é possível adicionar como responsável membros do projeto");
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.ProjectId.ValidationStatus.IsValid == false ||
                candidate.ProjectId.Equals(candidate.Responsible.ProjectId) == false ||
                candidate.Effort > Effort.MaxEffort())
            {
                candidate.AppendValidationResult(_responsibleFailure);
                return false;
            }

            return true;
        }
    }
}