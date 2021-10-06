using AppFabric.Domain.AggregationRelease.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class CreatedReleaseProjectionHandler : DomainEventHandler<ReleaseCreatedEvent>
    {
        protected override Task ExecuteHandle(ReleaseCreatedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
