using AppFabric.Domain.AggregationRelease.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class AddedActivityProjectionHandler : DomainEventHandler<ActivityAddedEvent>
    {
        protected override Task ExecuteHandle(ActivityAddedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
