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
        public EntityId ProjectId { get; }
        public int EstimatedHours { get; }

        public CloseActivityCommand(EntityId projectId, int estimatedHours)
        {
            this.ProjectId = projectId;
            EstimatedHours = estimatedHours;
        }
    }
}
