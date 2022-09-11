using System.Diagnostics;
using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationBilling
{
    public class BillingAggregationRoot : ObjectBasedAggregationRootWithEvents<Billing, EntityId>
    {
        public BillingAggregationRoot(Billing billing)
        {
            Debug.Assert(billing.IsValid);
            Apply(billing);
            
            if (billing.IsNew())
            {
                Raise(BillingCreatedEvent.For(billing));
            }

            AppendValidationResult(billing.Failures);
        }

        public void AddRelease(Release release, ISpecification<Billing> spec)
        {
            AggregateRootEntity.AddRelease(release);

            if (spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(ReleaseAddedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void Remove(ISpecification<Billing> spec)
        {
            if (spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(BillingRemovedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }
    }
}