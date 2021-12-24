using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class AssignResponsibleCommand
    {
        public AssignResponsibleCommand( Guid id, Guid memberId)
        {
            Id = EntityId.From(id);
            MemberId = EntityId.From(memberId);
        }
        public EntityId Id { get; set; }
        public EntityId MemberId { get; set; }
    }
}
