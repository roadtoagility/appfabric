using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.AggregationRelease.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationRelease
{
    public class ReleaseAggregationRoot : ObjectBasedAggregationRoot<Release, EntityId2>
    {
        private CompositeSpecification<Release> _spec;
        private ReleaseAggregationRoot(CompositeSpecification<Release> specification, Release release)
        {
            _spec = specification;

            if (_spec.IsSatisfiedBy(release))
            {
                Apply(release);

                if (release.IsNew())
                {
                    Raise(ReleaseCreatedEvent.For(release));
                }
            }

            AppendValidationResult(release.Failures);
        }

        private ReleaseAggregationRoot(CompositeSpecification<Release> specification, EntityId2 id, EntityId2 clientId)
            : this(specification, Release.NewRequest(id, clientId))
        {
        }

        #region Aggregation contruction


        public static ReleaseAggregationRoot ReconstructFrom(Release currentState)
        {
            var spec = new ReleaseSpecification();
            return new ReleaseAggregationRoot(spec, Release.From(currentState.Identity, currentState.ClientId,
                            VersionId.Next(currentState.Version)));
        }


        public static ReleaseAggregationRoot CreateFrom(EntityId2 releaseId, EntityId2 clientId)
        {
            var spec = new ReleaseSpecification();
            return new ReleaseAggregationRoot(spec, releaseId, clientId);
        }

        public void AddActivity(Activity activity)
        {
            var current = GetChange();
            current.AddActivity(activity);

            if (_spec.IsSatisfiedBy(current))
            {
                Apply(current);
                Raise(ActivityAddedEvent.For(current));
            }

            AppendValidationResult(current.Failures);
        }

        #endregion

        public void Remove()
        {
            //TODO: definir deleção
            if (_spec.IsSatisfiedBy(GetChange()))
            {
                Raise(ReleaseRemovedEvent.For(GetChange()));
            }
        }
    }
}
