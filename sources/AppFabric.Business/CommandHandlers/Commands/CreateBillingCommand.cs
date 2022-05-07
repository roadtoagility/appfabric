using System;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CreateBillingCommand : BaseCommand
    {
        public CreateBillingCommand(Guid id)
        {
            Id = EntityId.From(id);
        }

        public EntityId Id { get; set; }
    }
}