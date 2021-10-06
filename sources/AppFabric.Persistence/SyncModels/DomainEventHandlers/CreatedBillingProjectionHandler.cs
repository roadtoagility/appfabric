using AppFabric.Domain.AggregationBilling.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class CreatedBillingProjectionHandler : DomainEventHandler<BillingCreatedEvent>
    {
        protected override Task ExecuteHandle(BillingCreatedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
