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

        private ActivityAggregationRoot(EntityId id, Effort effort)
            : this(Activity.NewRequest(id, effort))
        {
        }

        #region Aggregation contruction


        public static ActivityAggregationRoot ReconstructFrom(Activity currentState)
        {
            return new ActivityAggregationRoot(Activity.From(currentState.Id, currentState.Effort,
                            BusinessObjects.Version.Next(currentState.Version)));
        }


        public static ActivityAggregationRoot CreateFrom(EntityId clientId, Effort effort)
        {
            return new ActivityAggregationRoot(EntityId.GetNext(), effort);
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

        public void UpdateRemaining(int hoursDebt)
        {
            var current = GetChange();
            var change = current.DecreaseEffort(hoursDebt);
            if (change.ValidationResults.IsValid)
            {
                Apply(change);
                Raise(EffortUpdatedEvent.For(change));
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
