using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.Framework.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class AddedActivityProjectionHandler : DomainEventHandler<ActivityAddedEvent>
    {
        protected override void ExecuteHandle(ActivityAddedEvent @event)
        {
            
        }
    }
}
