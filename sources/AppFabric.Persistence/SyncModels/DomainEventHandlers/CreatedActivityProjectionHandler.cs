using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class CreatedActivityProjectionHandler : DomainEventHandler<ActivityCreatedEvent>
    {
        protected override Task ExecuteHandle(ActivityCreatedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
