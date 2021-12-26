using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationBilling.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class CreatedBillingProjectionHandler : DomainEventHandler<BillingCreatedEvent>
    {
        protected override Task ExecuteHandle(BillingCreatedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}