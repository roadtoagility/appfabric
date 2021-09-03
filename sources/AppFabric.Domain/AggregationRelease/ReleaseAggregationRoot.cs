using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;

namespace AppFabric.Domain.AggregationRelease
{
    public class ReleaseAggregationRoot : AggregationRoot<Release>
    {
        private ReleaseAggregationRoot(Release release)
        {
            if (release.ValidationResults.IsValid)
            {
                Apply(release);

                if (release.IsNew())
                {
                    Raise(ReleaseCreatedEvent.For(release));
                }
            }

            ValidationResults = release.ValidationResults;
        }

        private ReleaseAggregationRoot(EntityId id, EntityId clientId)
            : this(Release.NewRequest(id, clientId))
        {
        }

        #region Aggregation contruction


        public static ReleaseAggregationRoot ReconstructFrom(Release currentState)
        {
            return new ReleaseAggregationRoot(Release.From(currentState.Id, currentState.ClientId,
                            BusinessObjects.Version.Next(currentState.Version)));
        }


        public static ReleaseAggregationRoot CreateFrom(EntityId releaseId, EntityId clientId)
        {
            return new ReleaseAggregationRoot(releaseId, clientId);
        }

        public void AddActivity(Activity activity)
        {
            var current = GetChange();
            var change = current.AddActivity(activity);
            if (change.ValidationResults.IsValid)
            {
                Apply(change);
                Raise(ActivityAddedEvent.For(change));
            }

            ValidationResults = change.ValidationResults;
        }

        #endregion

        public void Remove()
        {
            if (ValidationResults.IsValid)
            {
                Raise(ReleaseRemovedEvent.For(this.GetChange()));
            }
        }
    }
}
