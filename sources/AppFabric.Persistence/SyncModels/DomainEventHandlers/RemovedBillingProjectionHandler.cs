using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationBilling.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class RemovedBillingProjectionHandler : DomainEventHandler<BillingRemovedEvent>
    {
        protected override Task ExecuteHandle(BillingRemovedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}