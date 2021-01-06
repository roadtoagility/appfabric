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
using TodoAgility.Persistence.ReadModel.Projections;
using TodoAgility.Persistence.ReadModel.Repositories;

namespace TodoAgility.Persistence.SyncModels.DomainEventHandlers
{
    public class UpdateProjectProjectionHandler : DomainEventHandler<ProjectAddedEvent>
    {
        private readonly IDbSession<IProjectProjectionRepository> _projectSession;

        public UpdateProjectProjectionHandler(IDbSession<IProjectProjectionRepository> projectSession)
        {
            _projectSession = projectSession;
        }

        protected override void ExecuteHandle(ProjectAddedEvent @event)
        {
            var projection = new ProjectProjection(
                @event.Id.Value,
                @event.Name.Value,
                @event.Code.Value,
                @event.Budget.Value,
                @event.StartDate.Value,
                @event.ClientId.Value);
            
            _projectSession.Repository.Add(projection);
            
            //count releases
            // available budget
            // active tasks
            //finished tasks
            
            _projectSession.SaveChanges();
        }
    }
}