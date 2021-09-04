using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.Framework.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class DecreasedEffortProjectionHandler : DomainEventHandler<EffortDecreasedEvent>
    {
        protected override void ExecuteHandle(EffortDecreasedEvent @event)
        {
            
        }
    }
}
