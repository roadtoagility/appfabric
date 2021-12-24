using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class AddActivityCommand
    {
        public AddActivityCommand(Guid id, Guid activityId)
        {
            Id = EntityId.From(id);
            ActivityId = EntityId.From(activityId);

        }
        public EntityId Id { get; set; }
        public EntityId ActivityId { get; set; }
    }
}
