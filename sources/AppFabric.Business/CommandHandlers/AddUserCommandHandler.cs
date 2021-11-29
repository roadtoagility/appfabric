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
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;

namespace AppFabric.Business.CommandHandlers
{
    public sealed class AddUserCommandHandler : CommandHandler<AddUserCommand, CommandResult<Guid>>
    {
        private readonly IDbSession<IUserRepository> _dbSession;
        private readonly ILogger<AddUserCommandHandler> _logger;
        
        public AddUserCommandHandler(ILogger<AddUserCommandHandler> logger,IMediator publisher, IDbSession<IUserRepository> dbSession)
            :base(logger, publisher)
        {
            _logger = logger;
            _dbSession = dbSession;
        }
        
        protected override CommandResult<Guid> ExecuteCommand(AddUserCommand command)
        {
            var aggFactory = new AggregateFactory();
            var agg = aggFactory.Create(command);
            
            var isSucceed = false;
            var okId = Guid.Empty;
      
            if (agg.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                
                agg.GetEvents().ToImmutableList()
                    .ForEach( ev => Publisher.Publish(ev));
                
                isSucceed = true;
                okId = agg.GetChange().Id.Value;
            }
            
            return new CommandResult<Guid>(isSucceed, okId,agg.Failures.ToImmutableList());
        }
    }
}