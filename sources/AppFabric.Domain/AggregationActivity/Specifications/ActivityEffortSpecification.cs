﻿using DFlow.Domain.Specifications;
using FluentValidation.Results;

namespace AppFabric.Domain.BusinessObjects.Validations.ActivityRules
{
    public class ActivityEffortSpecification : CompositeSpecification<Activity>
    {
        private readonly ValidationFailure _effortFailure;

        public ActivityEffortSpecification()
        {
            _effortFailure = new ValidationFailure("IsEffortLessOrEqualEightHours"
                , "Uma atividade não pode ter esforço maior do que 8 horas");
        }

        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.Effort > Effort.MaxEffort())
            {
                candidate.AppendValidationResult(_effortFailure);
                return false;
            }

            return true;
        }
    }
}