using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationBilling.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class AddedReleaseProjectionHandler : DomainEventHandler<ReleaseAddedEvent>
    {
        protected override Task ExecuteHandle(ReleaseAddedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}