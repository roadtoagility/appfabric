using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class ClosedActivityProjectionHandler : DomainEventHandler<ActivityClosedEvent>
    {
        protected override Task ExecuteHandle(ActivityClosedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}