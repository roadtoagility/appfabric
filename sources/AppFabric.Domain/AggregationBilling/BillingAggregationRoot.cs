using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.AggregationBilling.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationBilling
{
    public class BillingAggregationRoot : ObjectBasedAggregationRoot<Billing, EntityId>
    {
        private readonly ISpecification<Billing> _spec;
        public  BillingAggregationRoot(ISpecification<Billing> specification, Billing billing)
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

        public void AddRelease(Release release)
        {
            AggregateRootEntity.AddRelease(release);

            if (_spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Apply(AggregateRootEntity);
                Raise(ReleaseAddedEvent.For(AggregateRootEntity));
            }

            AppendValidationResult(AggregateRootEntity.Failures);
        }

        public void Remove()
        {
            //TODO: definir deleção
            if (_spec.IsSatisfiedBy(AggregateRootEntity))
            {
                Raise(BillingRemovedEvent.For(AggregateRootEntity));
            }
        }
    }
}

