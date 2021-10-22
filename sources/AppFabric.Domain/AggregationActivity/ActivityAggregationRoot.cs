using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;
using System;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using System.Linq;
using DFlow.Domain.Specifications;
using AppFabric.Domain.AggregationActivity.Specifications;

namespace AppFabric.Domain.AggregationActivity
{    public class ActivityAggregationRoot : ObjectBasedAggregationRoot<Activity, EntityId>
    {
        private CompositeSpecification<Activity> _spec;
        private ActivityAggregationRoot(CompositeSpecification<Activity> specification, Activity activity)
        {
            _spec = specification;
            Apply(activity);

            if (activity.IsNew())
            {
                Raise(ActivityCreatedEvent.For(activity));
            }
        }

        #region Aggregation contruction

        public static ActivityAggregationRoot CreateFrom(EntityId projectId, int hours, CompositeSpecification<Activity> spec)
        {
            var activity = Activity.From(EntityId.GetNext(), projectId, hours, VersionId.New());
            return new ActivityAggregationRoot(spec, activity);
        }

        public static ActivityAggregationRoot ReconstructFrom(Activity activity, CompositeSpecification<Activity> spec)
        {
            return new ActivityAggregationRoot(spec, activity);
        }

        public void Asign(Member member)
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

        #endregion

        public void Remove()
        {
            if (GetChange().IsValid)
            {
                Raise(ActivityRemovedEvent.For(this.GetChange()));
            }
        }
    }
}
