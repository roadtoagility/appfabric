using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.AggregationRelease.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationRelease
{
    public class ReleaseAggregationRoot : ObjectBasedAggregationRoot<Release, EntityId>
    {
        private readonly ISpecification<Release> _spec;
        public ReleaseAggregationRoot(ISpecification<Release> specification, Release release)
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

        // #region Aggregation contruction
        //
        //
        // public static ReleaseAggregationRoot ReconstructFrom(Release currentState, CompositeSpecification<Release> spec)
        // {
        //     return new ReleaseAggregationRoot(spec, currentState);
        // }
        // #endregion

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
