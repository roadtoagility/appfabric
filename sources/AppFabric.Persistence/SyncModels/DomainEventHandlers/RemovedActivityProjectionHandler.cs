using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class RemovedActivityProjectionHandler : DomainEventHandler<ActivityRemovedEvent>
    {
        protected override Task ExecuteHandle(ActivityRemovedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}