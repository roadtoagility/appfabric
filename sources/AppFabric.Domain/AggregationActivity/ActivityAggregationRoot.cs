using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;


namespace AppFabric.Domain.AggregationActivity
{
    public class ActivityAggregationRoot : AggregationRoot<Activity>
    {
        private ActivityAggregationRoot(Activity activity)
        {
            if (activity.ValidationResults.IsValid)
            {
                Apply(activity);

                if (activity.IsNew())
                {
                    Raise(ActivityCreatedEvent.For(activity));
                }
            }

            ValidationResults = activity.ValidationResults;
        }

        private ActivityAggregationRoot(EntityId id, EntityId projectId, int hours)
            : this(Activity.NewRequest(id, projectId, hours))
        {
        }

        #region Aggregation contruction


        public static ActivityAggregationRoot ReconstructFrom(Activity currentState)
        {
            return new ActivityAggregationRoot(Activity.From(currentState.Id, currentState.ProjectId, currentState.Effort.Value,
                            BusinessObjects.Version.Next(currentState.Version)));
        }


        public static ActivityAggregationRoot CreateFrom(EntityId activityd, EntityId projectId, int hours)
        {
            return new ActivityAggregationRoot(activityd, projectId, hours);
        }

        public void Asign(Member member)
        {
            var current = GetChange();
            var change = current.AddMember(member);
            if (change.ValidationResults.IsValid)
            {
                Apply(change);
                Raise(MemberAsignedEvent.For(change));
            }

            ValidationResults = change.ValidationResults;
        }

        public void UpdateRemaining(int hours)
        {
            var current = GetChange();
            var oldEffort = current.Effort.Value;
            var change = current.UpdateEffort(hours);
            if (change.ValidationResults.IsValid)
            {
                Apply(change);

                if(oldEffort > hours)
                {
                    Raise(EffortDecreasedEvent.For(change));
                }
                else
                {
                    Raise(EffortIncreasedEvent.For(change));
                }
                
            }

            ValidationResults = change.ValidationResults;
        }

        public void Close()
        {
            var current = GetChange();

            var change = current.Close();
            if (change.ValidationResults.IsValid)
            {
                Apply(change);
                Raise(ActivityClosedEvent.For(change));
            }

            ValidationResults = change.ValidationResults;
        }

        #endregion

        public void Remove()
        {
            if (ValidationResults.IsValid)
            {
                Raise(ActivityRemovedEvent.For(this.GetChange()));
            }
        }
    }
}
