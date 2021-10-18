﻿using AppFabric.Domain.Framework.BusinessObjects;
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
        public EntityId2 Id { get; set; }

        public CreateBillingCommand(Guid id)
        {
            Id = EntityId2.From(id);
        }
    }
}
