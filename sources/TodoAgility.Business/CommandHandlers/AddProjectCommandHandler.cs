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

using System.Collections.Immutable;
using FluentMediator;
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.Framework;
using TodoAgility.Domain.AggregationProject;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Domain.Framework.DomainEvents;
using TodoAgility.Persistence;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Framework.Repositories;
using TodoAgility.Persistence.Model.Repositories;

namespace TodoAgility.Business.CommandHandlers
{
    public sealed class AddProjectCommandHandler : CommandHandler<AddProjectCommand, ExecutionResult>
    {
        private readonly IDbSession<IProjectRepository> _dbSession;
        
        public AddProjectCommandHandler(IMediator publisher, IDbSession<IProjectRepository> dbSession)
            :base(publisher)
        {
            _dbSession = dbSession;
        }
        
        protected override ExecutionResult ExecuteCommand(AddProjectCommand command)
        {
            var agg = ProjectAggregationRoot.CreateFrom(EntityId.GetNext(), command.Name,
                command.Code, command.Budget, command.StartDate, command.ClientId);
            var isSucceed = false;
      
            if (agg.ValidationResults.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                agg.GetEvents().ToImmutableList().ForEach( ev => Publisher.Publish(ev));
                isSucceed = true;
            }
            
            return new ExecutionResult(isSucceed, agg.ValidationResults.Errors.ToImmutableList());
        }
    }
}