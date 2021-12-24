﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class AddReleaseCommand
    {
        public AddReleaseCommand(Guid id, Guid releaseId)
        {
            Id = EntityId.From(id);
            ReleaseId = EntityId.From(releaseId);
        }
        public EntityId Id { get; set; }
        public EntityId ReleaseId { get; set; }
    }
}
