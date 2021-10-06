using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class AsignedMemberProjectionHandler : DomainEventHandler<MemberAsignedEvent>
    {
        protected override Task ExecuteHandle(MemberAsignedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.Exception();
        }
    }
}
