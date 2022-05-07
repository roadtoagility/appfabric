using System.Linq;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationRelease.Specifications
{
    public class ReleaseSpecification : CompositeSpecification<Release>
    {
        public override bool IsSatisfiedBy(Release candidate)
        {
            var haveNotClosed = candidate.Activities.Any(x => x.ActivityStatus != ActivityStatus.Closed());
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