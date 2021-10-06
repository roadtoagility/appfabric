using AppFabric.Domain.AggregationRelease.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class RemovedReleaseProjectionHandler : DomainEventHandler<ReleaseRemovedEvent>
    {
        protected override Task ExecuteHandle(ReleaseRemovedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
