// Copyright (C) 2020  Road to Agility
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Library General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Library General Public License for more details.
//
// You should have received a copy of the GNU Library General Public
// License along with this library; if not, write to the
// Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
// Boston, MA  02110-1301, USA.
//

using System;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class UpdateProjectCommand:BaseCommand
    {
        public UpdateProjectCommand(Guid id, string name,
            decimal budget,
            string owner,
            string orderNumber,
            string status
        )
        {
            Id = EntityId.From(id);
            Name = ProjectName.From(name);
            Budget = Money.From(budget);
            Owner = Email.From(owner);
            OrderNumber = ServiceOrder.From((orderNumber,false));
            Status = ProjectStatus.From(status);
        }

        public EntityId Id { get; set; }
        public ProjectName Name { get; set; }
        public Money Budget { get; set; }
        public Email Owner { get; set; }
        public ServiceOrder OrderNumber { get; set; }
        public ProjectStatus Status { get; set; }

    }
}