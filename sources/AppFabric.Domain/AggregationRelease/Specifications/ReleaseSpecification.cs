using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationRelease.Specifications
{
    public class ReleaseSpecification : CompositeSpecification<Release>
    {
        public override bool IsSatisfiedBy(Release candidate)
        {
            var haveNotClosed = candidate.Activities.Any(x => x.ActivityStatus.Value != (int)ActivityStatus.Status.Closed);
            if (haveNotClosed)
            {
                candidate.AppendValidationResult(new ValidationFailure("ReleaseSpecification",
                    "Todas as atividades de uma release devem ter sido concluídas."));
                return false;
            }

            return true;
        }
    }
}
