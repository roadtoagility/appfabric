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
using System.Collections.Immutable;
using FluentMediator;
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.Framework;
using TodoAgility.Domain.AggregationProject;
using TodoAgility.Domain.BusinessObjects;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Model.Repositories;

namespace TodoAgility.Business.CommandHandlers
{
    public sealed class AddProjectCommandHandler : CommandHandler<AddProjectCommand, CommandResult<Guid>>
    {
        private readonly IDbSession<IProjectRepository> _dbSession;
        
        public AddProjectCommandHandler(IMediator publisher, IDbSession<IProjectRepository> dbSession)
            :base(publisher)
        {
            _dbSession = dbSession;
        }
        
        protected override CommandResult<Guid> ExecuteCommand(AddProjectCommand command)
        {
            // _dbSession.Repository.Get(id);
                
            var agg = ProjectAggregationRoot.CreateFrom(
                ProjectName.From(command.Name),
                ProjectCode.From(command.Code), 
                Money.From(command.Budget), 
                DateAndTime.From(command.StartDate), 
                EntityId.From(command.ClientId));
            
            var isSucceed = false;
            var okId = Guid.Empty;
      
            if (agg.ValidationResults.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                agg.GetEvents().ToImmutableList().ForEach( ev => Publisher.Publish(ev));
                isSucceed = true;
                okId = agg.GetChange().Id.Value;
            }
            
            return new CommandResult<Guid>(isSucceed,okId, agg.ValidationResults.Errors.ToImmutableList());
        }
    }
}