﻿using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivitySpecification : CompositeSpecification<Activity>
    {
        public override bool IsSatisfiedBy(Activity candidate)
        {
            //TODO: fiz e sai correndo :) Arrumar essa meleca o quanto antes
            var isValid = true;
            if (IsClosedOnlyWithoutEffort(candidate))
            {
                candidate.AppendValidationResult(new ValidationFailure("IsClosedOnlyWithoutEffort",
                    "The candidate already exists."));
                isValid = false;
            }
            if (IsEffortLessOrEqualEightHours(candidate))
            {
                candidate.AppendValidationResult(new ValidationFailure("IsEffortLessOrEqualEightHours",
                    "Uma atividade não pode ter esforço maior do que 8 horas"));
                isValid = false;
            }
            if (CanHaveResponsible(candidate))
            {
                candidate.AppendValidationResult(new ValidationFailure("CanHaveResponsible",
                    "Só é possível adicionar como responsável membros do projeto"));
                isValid = false;
            }
            if (CanHaveResponsible(candidate))
            {
                candidate.AppendValidationResult(new ValidationFailure("CanHaveResponsible",
                    "Só é possível adicionar como responsável membros do projeto"));
                isValid = false;
            }
            if (CanBeClosed(candidate))
            {
                candidate.AppendValidationResult(new ValidationFailure("CanHaveResponsible",
                    "Não é possível fechar uma atividade com esforço pendente"));
                isValid = false;
            }

            return isValid;
        }

        private bool CanBeClosed(Activity activity)
        {
            if (activity.ActivityStatus.Value == (int)ActivityStatus.Status.Closed && activity.Effort.Value > 0)
                return false;
            return true;
        }

        private bool CanHaveResponsible(Activity activity)
        {
            if (activity.Responsible.ProjectId.Value != Guid.Empty && activity.ProjectId != activity.Responsible.ProjectId)
            {
                return false;
            }

            return true;
        }

        private bool IsEffortLessOrEqualEightHours(Activity activity)
        {
            if (activity.Effort.Value > 8)
                return false;
            return true;
        }

        private bool IsClosedOnlyWithoutEffort(Activity activity)
        {
            if (activity.ActivityStatus == ActivityStatus.From(2) && activity.Effort.Value > 0)
                return false;
            return true;
        }
    }
}