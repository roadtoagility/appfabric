using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class DecreasedEffortProjectionHandler : DomainEventHandler<EffortDecreasedEvent>
    {
        protected override Task ExecuteHandle(EffortDecreasedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
