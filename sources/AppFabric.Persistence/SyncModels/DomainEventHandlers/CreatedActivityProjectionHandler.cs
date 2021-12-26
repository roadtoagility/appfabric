using System;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class CreatedActivityProjectionHandler : DomainEventHandler<ActivityCreatedEvent>
    {
        protected override Task ExecuteHandle(ActivityCreatedEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}