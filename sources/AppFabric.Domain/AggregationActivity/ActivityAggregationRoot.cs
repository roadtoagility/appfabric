using System.Diagnostics;
using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Domain.Specifications;
using Activity = AppFabric.Domain.BusinessObjects.Activity;

namespace AppFabric.Domain.AggregationActivity
{
    public sealed class ActivityAggregationRoot : ObjectBasedAggregationRootWithEvents<Activity, EntityId>
    {
        public ActivityAggregationRoot(Activity activity)
        {
            Debug.Assert(activity.IsValid);
            Apply(activity);
            
            if (activity.IsNew())
            {
                Raise(ActivityCreatedEvent.For(activity));
            }
        }

        public void Assign(Member member, ISpecification<Activity> spec)
        {
            AggregateRootEntity.AddMember(member);

            if (spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(MemberAssignedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void UpdateRemaining(Effort newEffortHours, ISpecification<Activity> spec)
        {
            AggregateRootEntity.UpdateEffort(AggregateRootEntity.Effort);

            if (spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);

                if (AggregateRootEntity.Effort > newEffortHours)
                    Raise(EffortDecreasedEvent.For(AggregateRootEntity));
                else
                    Raise(EffortIncreasedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void Close(ISpecification<Activity> spec)
        {
            AggregateRootEntity.Close();
            if (spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(ActivityClosedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void Remove(ISpecification<Activity> spec)
        {
            if (spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(ActivityRemovedEvent.For(GetChange()));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }
    }
}