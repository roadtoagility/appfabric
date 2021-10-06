using AppFabric.Domain.AggregationBilling.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class AddedReleaseProjectionHandler : DomainEventHandler<ReleaseAddedEvent>
    {
        protected override Task ExecuteHandle(ReleaseAddedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
