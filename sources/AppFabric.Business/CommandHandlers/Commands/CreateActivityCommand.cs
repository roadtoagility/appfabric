using System;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CreateActivityCommand : BaseCommand
    {
        public CreateActivityCommand(Guid projectId, int estimatedHours)
        {
            ProjectId = EntityId.From(projectId);
            EstimatedHours = Effort.From(estimatedHours);
        }

        public EntityId ProjectId { get; }
        public Effort EstimatedHours { get; }
    }
}