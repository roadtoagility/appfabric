using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class DecreasedEffortProjectionHandler : DomainEventHandler<EffortDecreasedEvent>
    {
        protected override Task ExecuteHandle(EffortDecreasedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}