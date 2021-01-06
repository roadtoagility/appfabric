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


using TodoAgility.Domain.AggregationProject.Events;
using TodoAgility.Domain.Framework.DomainEvents;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Repositories;

namespace TodoAgility.Persistence.DomainEventHandlers
{
    public class ProjectAddedHandler : DomainEventHandler<ProjectAddedEvent>
    {
        private readonly IDbSession<IProjectRepository> _projectSession;

        public ProjectAddedHandler(IDbSession<IProjectRepository> projectSession)
        {
            _projectSession = projectSession;
        }

        protected override void ExecuteHandle(ProjectAddedEvent @event)
        {
            var project = _projectSession.Repository.Get(@event.Code);

            // var activity = new List<EntityId> {ev?.Id};
            // var projectWithTasks = Project.CombineProjectAndActivities(project, activity);
            
            //_projectSession.Repository.Add(projectWithTasks);
            _projectSession.SaveChanges();
        }
    }
}