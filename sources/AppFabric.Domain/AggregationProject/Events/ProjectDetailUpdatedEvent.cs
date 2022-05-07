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
using DFlow.Domain.DomainEvents;

namespace AppFabric.Domain.AggregationProject.Events
{
    public class ProjectDetailUpdatedEvent : DomainEvent
    {
        private ProjectDetailUpdatedEvent(EntityId id, ProjectName name, Email owner, ProjectStatus status,
            Money budget, ServiceOrder orderNumber, VersionId version)
            : base(DateTime.Now, version)
        {
            Id = id;
            Name = name;
            Owner = owner;
            Status = status;
            OrderNumber = orderNumber;
            Budget = budget;
        }

        public EntityId Id { get; }
        public ProjectName Name { get; }
        public ProjectStatus Status { get; }

        public Money Budget { get; }

        public Email Owner { get; }

        public ServiceOrder OrderNumber { get; }

        public static ProjectDetailUpdatedEvent For(Project project)
        {
            return new ProjectDetailUpdatedEvent(
                project.Identity,
                project.Name,
                project.Owner,
                project.Status,
                project.Budget,
                project.OrderNumber,
                project.Version);
        }
    }
}