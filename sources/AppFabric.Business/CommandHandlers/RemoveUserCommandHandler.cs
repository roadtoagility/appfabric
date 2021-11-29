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
    public sealed class RemoveUserCommandHandler : CommandHandler<RemoveUserCommand, ExecutionResult>
    {
        private readonly IDbSession<IUserRepository> _userDb;
        private readonly ILogger<RemoveUserCommandHandler> _logger;
        private readonly AggregateFactory _factory;

        public RemoveUserCommandHandler(ILogger<RemoveUserCommandHandler> logger, 
            IMediator publisher, 
            IDbSession<IUserRepository> userDb,
            AggregateFactory factory)
            :base(logger,publisher)
        {
            _userDb = userDb;
            _logger = logger;
            _factory = factory;
        }
        
        protected override ExecutionResult ExecuteCommand(RemoveUserCommand command)
        {
            var user = _userDb.Repository.Get(EntityId.From(command.Id));
            var agg = _factory.Create(new LoadUserCommand(user));//UserAggregationRoot.ReconstructFrom(user);
            var isSucceed = false;
      
            if (agg.IsValid)
            {
                agg.Remove();
                
                _userDb.Repository.Remove(agg.GetChange());
                _userDb.SaveChanges();
                agg.GetEvents().ToImmutableList().ForEach( ev => Publisher.Publish(ev));
                isSucceed = true;
            }
            
            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}