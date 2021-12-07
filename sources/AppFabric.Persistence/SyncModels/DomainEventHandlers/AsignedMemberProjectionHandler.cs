using AppFabric.Domain.AggregationActivity.Events;
using DFlow.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class AsignedMemberProjectionHandler : DomainEventHandler<MemberAssignedEvent>
    {
        protected override Task ExecuteHandle(MemberAssignedEvent @event, CancellationToken cancellationToken)
        {
            throw new System.Exception();
        }
    }
}
