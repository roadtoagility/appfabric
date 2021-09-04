using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.Framework.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class RemovedReleaseProjectionHandler : DomainEventHandler<ReleaseRemovedEvent>
    {
        protected override void ExecuteHandle(ReleaseRemovedEvent @event)
        {
            
        }
    }
}
