using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CreateBillingCommand : BaseCommand
    {
        public EntityId Id { get; set; }

        public CreateBillingCommand(Guid id)
        {
            Id = EntityId.From(id);
        }
    }
}
