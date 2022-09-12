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
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Events.DomainEvents;

namespace AppFabric.Domain.AggregationProject.Events
{
    public class ProjectAddedEvent : DomainEvent
    {
        private ProjectAddedEvent(EntityId id, ProjectName name, ProjectCode code, DateAndTime startDate, Money budget,
            EntityId clientId, VersionId version)
            : base(DateTime.Now, version)
        {
            Id = id;
            Code = code;
            Name = name;
            Budget = budget;
            StartDate = startDate;
            ClientId = clientId;
            Owner = Email.Empty();
            Status = ProjectStatus.Default();
            OrderNumber = ServiceOrder.Empty();
        }

        public EntityId Id { get; }
        public ProjectName Name { get; }
        public ProjectCode Code { get; }

        public Money Budget { get; }

        public EntityId ClientId { get; }

        public DateAndTime StartDate { get; }

        public Email Owner { get; }
        public ServiceOrder OrderNumber { get; }
        public ProjectStatus Status { get; }

        public static ProjectAddedEvent For(Project project)
        {
            return new ProjectAddedEvent(
                project.Identity,
                project.Name,
                project.Code,
                project.StartDate,
                project.Budget,
                project.ClientId,
                project.Version);
        }
    }
}