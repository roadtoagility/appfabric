using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.Framework.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Persistence.SyncModels.DomainEventHandlers
{
    public class CreatedBillingProjectionHandler : DomainEventHandler<BillingCreatedEvent>
    {
        protected override void ExecuteHandle(BillingCreatedEvent @event)
        {
            
        }
    }
}
