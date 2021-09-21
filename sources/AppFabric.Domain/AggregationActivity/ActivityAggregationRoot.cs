using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;
using System;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using System.Linq;

namespace AppFabric.Domain.AggregationActivity
{
    public class ActivityAggregationRoot : ObjectBasedAggregationRoot<Activity, EntityId2>
    {

        private ActivityAggregationRoot(Activity activity)
        {
            if (activity.IsValid)
            {
                Apply(activity);

                if (activity.IsNew())
                {
                    Raise(ActivityCreatedEvent.For(activity));
                }
            }

            AppendValidationResult(activity.Failures);
        }

        #region Aggregation contruction

        public static ActivityAggregationRoot CreateFrom(EntityId2 projectId, int hours)
        {
            var activity = Activity.From(EntityId2.GetNext(), projectId, hours, VersionId.New());
            return new ActivityAggregationRoot(activity);
        }

        public void Asign(Member member)
        {
            var current = GetChange();
            current.AddMember(member);
            if (current.IsValid)
            {
                Apply(current);
                Raise(MemberAsignedEvent.For(current));
            }
        }

        public void UpdateRemaining(int hours)
        {
            var current = GetChange();
            var oldEffort = current.Effort.Value;
            current.UpdateEffort(hours);
            if (current.IsValid)
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
            if (current.IsValid)
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
