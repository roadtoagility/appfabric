﻿using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationActivity.Specifications
{
    public class ActivityCanBeAddedToRelease : CompositeSpecification<Activity>
    {
        public override bool IsSatisfiedBy(Activity candidate)
        {
            if (candidate.ActivityStatus.Equals(ActivityStatus.Closed()) ||
                candidate.Effort.Equals(Effort.WithoutEffort()))
            {
                return false;
            }

            return true;
        }
    }
}
