using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class RemovedActivityProjectionHandler : DomainEventHandler<ActivityRemovedEvent>
    {
        protected override Task ExecuteHandle(ActivityRemovedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
