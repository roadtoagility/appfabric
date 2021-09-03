﻿using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;

namespace AppFabric.Domain.AggregationBilling
{
    public class BillingAggregationRoot : AggregationRoot<Billing>
    {
        private BillingAggregationRoot(Billing billing)
        {
            if (billing.ValidationResults.IsValid)
            {
                Apply(billing);

                if (billing.IsNew())
                {
                    Raise(BillingCreatedEvent.For(billing));
                }
            }

            ValidationResults = billing.ValidationResults;
        }

        private BillingAggregationRoot(EntityId id)
            : this(Billing.NewRequest(id))
        {
        }

        #region Aggregation contruction


        public static BillingAggregationRoot ReconstructFrom(Billing currentState)
        {
            return new BillingAggregationRoot(Billing.From(currentState.Id,
                            BusinessObjects.Version.Next(currentState.Version)));
        }


        public static BillingAggregationRoot CreateFrom(EntityId releaseId)
        {
            return new BillingAggregationRoot(releaseId);
        }

        public void AddRelease(Release release)
        {
            var current = GetChange();
            var change = current.AddRelease(release);
            if (change.ValidationResults.IsValid)
            {
                Apply(change);
                Raise(ReleaseAddedEvent.For(change));
            }

            ValidationResults = change.ValidationResults;
        }

        #endregion

        public void Remove()
        {
            if (ValidationResults.IsValid)
            {
                Raise(BillingRemovedEvent.For(this.GetChange()));
            }
        }
    }
}

