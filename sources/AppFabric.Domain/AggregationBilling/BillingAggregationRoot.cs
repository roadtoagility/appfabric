using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.AggregationBilling.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationBilling
{
    public class BillingAggregationRoot : ObjectBasedAggregationRoot<Billing, EntityId2>
    {
        private CompositeSpecification<Billing> _spec;
        private BillingAggregationRoot(CompositeSpecification<Billing> specification, Billing billing)
        {
            _spec = specification;
            if (_spec.IsSatisfiedBy(billing))
            {
                Apply(billing);

                if (billing.IsNew())
                {
                    Raise(BillingCreatedEvent.For(billing));
                }
            }

            AppendValidationResult(billing.Failures);
        }

        private BillingAggregationRoot(CompositeSpecification<Billing> specification, EntityId2 id)
            : this(specification, Billing.NewRequest(id))
        {

        }

        #region Aggregation contruction


        public static BillingAggregationRoot ReconstructFrom(Billing currentState)
        {
            var spec = new BillingSpecification();
            return new BillingAggregationRoot(spec, Billing.From(currentState.Identity,
                            VersionId.Next(currentState.Version)));
        }


        public static BillingAggregationRoot CreateFrom(EntityId2 billingId)
        {
            var spec = new BillingSpecification();
            return new BillingAggregationRoot(spec, billingId);
        }

        public void AddRelease(Release release)
        {
            var current = GetChange();
            current.AddRelease(release);

            if (_spec.IsSatisfiedBy(current))
            {
                Apply(current);
                Raise(ReleaseAddedEvent.For(current));
            }

            AppendValidationResult(current.Failures);
        }

        #endregion

        public void Remove()
        {
            //TODO: definir deleção
            if (_spec.IsSatisfiedBy(GetChange()))
            {
                Raise(BillingRemovedEvent.For(GetChange()));
            }
        }
    }
}

