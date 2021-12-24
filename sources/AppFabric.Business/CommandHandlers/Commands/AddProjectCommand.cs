﻿// Copyright (C) 2020  Road to Agility
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

using DFlow.Domain.Command;
using System;
using System.Collections.Immutable;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class AddProjectCommand : BaseCommand
    {
        public AddProjectCommand(string name,string owner,string code,DateTime startDate,decimal budget,
            Guid clientId,string serviceOrderNumber,bool serviceOrderStatus,string status)
        {
            Name = ProjectName.From(name);
            ServiceOrderNumber = ServiceOrder.From((serviceOrderNumber, serviceOrderStatus));
            Status = ProjectStatus.From(status);
            Code = ProjectCode.From(code);
            StartDate = DateAndTime.From(startDate);
            Budget = Money.From(budget);
            ClientId = EntityId.From(clientId);
            Owner = Email.From(owner);
            
            AppendValidationResult(Name.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(ServiceOrderNumber.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(Status.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(Code.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(StartDate.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(Budget.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(ClientId.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(Owner.ValidationStatus.Errors.ToImmutableList());
        }
        
        public ProjectName Name { get; set; }
        public Email Owner { get; set; }
        public ProjectCode Code { get; set; }
        public DateAndTime StartDate { get; set; }
        public Money Budget { get; set; }
        public EntityId ClientId { get; set; }
        public ServiceOrder ServiceOrderNumber { get; set; }
        public ProjectStatus Status { get; set; }
    }
}