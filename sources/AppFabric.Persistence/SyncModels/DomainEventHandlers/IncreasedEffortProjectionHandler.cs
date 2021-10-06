using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class IncreasedEffortProjectionHandler : DomainEventHandler<EffortIncreasedEvent>
    {
        protected override Task ExecuteHandle(EffortIncreasedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
