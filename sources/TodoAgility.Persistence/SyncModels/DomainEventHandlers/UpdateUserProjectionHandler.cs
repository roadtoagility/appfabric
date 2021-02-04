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
using TodoAgility.Domain.AggregationUser.Events;
using TodoAgility.Domain.Framework.DomainEvents;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.ReadModel;
using TodoAgility.Persistence.ReadModel.Repositories;

namespace TodoAgility.Persistence.SyncModels.DomainEventHandlers
{
    public sealed class UpdateUserProjectionHandler : DomainEventHandler<UserAddedEvent>
    {
        private readonly IDbSession<IUserProjectionRepository> _projectSession;

        public UpdateUserProjectionHandler(IDbSession<IUserProjectionRepository> projectSession)
        {
            _projectSession = projectSession;
        }

        protected override void ExecuteHandle(UserAddedEvent @event)
        {
            var projection = new UserProjection(
                @event.Id.Value,
                @event.Name.Value,
                @event.Cnpj.Value,
                @event.CommercialEmail.Value);
            
            _projectSession.Repository.Add(projection);
            
            //count releases
            // available budget
            // active tasks
            //finished tasks
            
            _projectSession.SaveChanges();
        }
    }
}