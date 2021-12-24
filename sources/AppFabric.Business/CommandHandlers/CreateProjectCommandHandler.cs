// Copyright (C) 2021  Road to Agility
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

using FluentMediator;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Domain.Events;
using DFlow.Persistence;
using FluentValidation.Results;

namespace AppFabric.Business.CommandHandlers
{
    public class CreateProjectCommandHandler : CommandHandler<CreateProjectCommand, CommandResult<Guid>>
    {
        private readonly IDbSession<IProjectRepository> _dbSession;

        public CreateProjectCommandHandler(IDomainEventBus publisher, IDbSession<IProjectRepository> dbSession)
            : base(publisher)
        {
            _dbSession = dbSession;
        }

        protected override Task<CommandResult<Guid>> ExecuteCommand(
            CreateProjectCommand command, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
