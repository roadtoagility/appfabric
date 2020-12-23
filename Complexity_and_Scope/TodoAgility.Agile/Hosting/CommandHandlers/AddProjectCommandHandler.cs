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

using TodoAgility.Agile.CQRS.Framework;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.AggregationProject;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Hosting.CommandHandlers;
using TodoAgility.Agile.Hosting.Framework;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.CQRS.CommandHandlers
{
    public sealed class AddProjectCommandHandler : CommandHandler<AddProjectCommand, ExecutionResult>
    {
        private readonly IDbSession<IProjectRepository> _dbSession;

        public AddProjectCommandHandler(IEventDispatcher publisher, IDbSession<IProjectRepository> dbSession)
            :base(publisher)
        {
            _dbSession = dbSession;
        }

        protected override ExecutionResult ExecuteCommand(AddProjectCommand command)
        {
            var agg = ProjectAggregationRoot.CreateFrom(command.Description, EntityId.GetNext());

            _dbSession.Repository.Add(agg.GetChange());
            _dbSession.SaveChanges();
            Publisher.Publish(agg.GetEvents());
            
            return new ExecutionResult(true);
        }
    }
}