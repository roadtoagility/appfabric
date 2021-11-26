using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationActivity
{    public class ActivityAggregationRoot : ObjectBasedAggregationRoot<Activity, EntityId>
    {
        private readonly ISpecification<Activity> _spec;
        public ActivityAggregationRoot(ISpecification<Activity> specification, Activity activity)
        {
            _spec = specification;
            Apply(activity);

            if (activity.IsNew())
            {
                Raise(ActivityCreatedEvent.For(activity));
            }
        }
        
        public void Assign(Member member)
        {
            AggregateRootEntity.AddMember(member);

            if (_spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(MemberAsignedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void UpdateRemaining(int hours)
        {
            var oldEffort = AggregateRootEntity.Effort.Value;
            AggregateRootEntity.UpdateEffort(hours);
            if (_spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);

                if(oldEffort > hours)
                {
                    Raise(EffortDecreasedEvent.For(AggregateRootEntity));
                }
                else
                {
                    Raise(EffortIncreasedEvent.For(AggregateRootEntity));
                }
            }
            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void Close()
        {
            AggregateRootEntity.Close();
            if (_spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(ActivityClosedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }
        
        public void Remove()
        {
            if (GetChange().IsValid)
            {
                Raise(ActivityRemovedEvent.For(this.GetChange()));
            }
        }
    }
}
