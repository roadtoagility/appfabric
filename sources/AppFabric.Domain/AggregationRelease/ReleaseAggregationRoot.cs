using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationRelease
{
    public class ReleaseAggregationRoot : ObjectBasedAggregationRoot<Release, EntityId>
    {
        public ReleaseAggregationRoot(Release release)
        {
            Apply(release);

            if (release.IsNew()) Raise(ReleaseCreatedEvent.For(release));
        }

        public void AddActivity(Activity activity, ISpecification<Activity> spec)
        {
            if (spec.IsSatisfiedBy(activity))
            {
                AggregateRootEntity.AddActivity(activity);
                Apply(AggregateRootEntity);
                Raise(ActivityAddedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void Remove(Activity activity, ISpecification<Release> spec)
        {
            AggregateRootEntity.RemoveActivity(activity);

            if (spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(ReleaseRemovedEvent.For(AggregateRootEntity));
            }
        }
    }
}