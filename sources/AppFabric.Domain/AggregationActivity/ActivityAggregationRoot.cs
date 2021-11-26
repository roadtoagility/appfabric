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
            var current = GetChange();
            current.AddMember(member);

            if (_spec.IsSatisfiedBy(current))
            {
                Apply(current);
                Raise(MemberAsignedEvent.For(current));
            }

            AppendValidationResult(current.Failures);
        }

        public void UpdateRemaining(int hours)
        {
            var current = GetChange();
            var oldEffort = current.Effort.Value;
            current.UpdateEffort(hours);
            if (_spec.IsSatisfiedBy(current))
            {
                Apply(current);

                if(oldEffort > hours)
                {
                    Raise(EffortDecreasedEvent.For(current));
                }
                else
                {
                    Raise(EffortIncreasedEvent.For(current));
                }
            }
            AppendValidationResult(current.Failures);
        }

        public void Close()
        {
            var current = GetChange();

            current.Close();
            if (_spec.IsSatisfiedBy(current))
            {
                Apply(current);
                Raise(ActivityClosedEvent.For(current));
            }

            AppendValidationResult(current.Failures);
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
