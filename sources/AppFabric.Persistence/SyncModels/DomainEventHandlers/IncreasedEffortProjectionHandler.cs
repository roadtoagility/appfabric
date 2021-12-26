using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class IncreasedEffortProjectionHandler : DomainEventHandler<EffortIncreasedEvent>
    {
        protected override Task ExecuteHandle(EffortIncreasedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}