using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CreateActivityCommand : BaseCommand
    {
        public EntityId ProjectId { get; }
        public int EstimatedHours { get; }

        public CreateActivityCommand(EntityId projectId, int estimatedHours)
        {
            ProjectId = projectId;
            EstimatedHours = estimatedHours;
        }
    }
}
