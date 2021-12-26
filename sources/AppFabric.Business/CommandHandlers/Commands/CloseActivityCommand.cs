using DFlow.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CloseActivityCommand : BaseCommand
    {
        public EntityId ActivityId { get; }
        public Effort EstimatedHours { get; }

        public CloseActivityCommand(EntityId activityId, int estimatedHours)
        {
            ActivityId = activityId;
            EstimatedHours = Effort.From(estimatedHours);
        }
    }
}
