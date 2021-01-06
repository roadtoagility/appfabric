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
using TodoAgility.Domain.BusinessObjects;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Domain.Framework.DomainEvents;

namespace TodoAgility.Domain.AggregationProject.Events
{
    public class ProjectAddedEvent : DomainEvent
    {
        private ProjectAddedEvent(ProjectName name, ProjectCode code, DateAndTime startDate, Money budget, EntityId clientId)
            : base(DateTime.Now)
        {
            Code = code;
            Name = name;
            Budget = budget;
            StartDate = startDate;
            ClientId = clientId;
        }

        public ProjectName Name { get; }
        public ProjectCode Code { get; }
        
        public Money Budget { get; }
        
        public EntityId ClientId { get; }

        public DateAndTime StartDate { get; }
        public static ProjectAddedEvent For(Project project)
        {
            return new ProjectAddedEvent(project.Name,
                project.Code,
                project.StartDate, 
                project.Budget, 
                project.ClientId);
        }
    }
}