using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CloseActivityCommand : BaseCommand
    {
        public EntityId2 ProjectId { get; }
        public int EstimatedHours { get; }

        public CloseActivityCommand(EntityId2 projectId, int estimatedHours)
        {
            this.ProjectId = projectId;
            EstimatedHours = estimatedHours;
        }
    }
}
