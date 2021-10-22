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
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;

namespace AppFabric.Business.CommandHandlers
{
    public sealed class AddProjectCommandHandler : CommandHandler<AddProjectCommand, CommandResult<Guid>>
    {
        private readonly IDbSession<IUserRepository> _dbUserSession;
        private readonly IDbSession<IProjectRepository> _dbSession;
        private readonly ILogger<AddProjectCommandHandler> _logger;
        private readonly AggregateFactory _factory;

        public AddProjectCommandHandler(ILogger<AddProjectCommandHandler> logger, IMediator publisher,
            IDbSession<IProjectRepository> dbSession
            , IDbSession<IUserRepository> dbUserSession,
            AggregateFactory factory)
            : base(logger, publisher)
        {
            _logger = logger;
            _dbSession = dbSession;
            _dbUserSession = dbUserSession;
            _factory = factory;
        }

        protected override CommandResult<Guid> ExecuteCommand(AddProjectCommand command)
        {
            var isSucceed = false;
            var aggregationId = Guid.Empty;

            _logger.LogDebug("Criada agregação a partir do comando {CommandName} com valores {Valores}",
                nameof(command), command);

            var client = _dbUserSession.Repository.Get(EntityId.From(command.ClientId));

            var agg = _factory.Create(command);

            if (agg.IsValid)
            {
                // _logger.LogInformation($"Agregação Project valida id gerado", agg.GetChange().Id);

                using (_logger.BeginScope("Persistencia"))
                {
                    _dbSession.Repository.Add(agg.GetChange());
                    _dbSession.SaveChanges();
                }

                // _logger.LogInformation($"Project persistido ID: {agg.GetChange().Id}");
                using (_logger.BeginScope("Publicacão de Eventos"))
                {
                    agg.GetEvents().ToImmutableList().ForEach(ev => Publisher.Publish(ev));
                }

                isSucceed = true;
                aggregationId = agg.GetChange().Id.Value;
            }

            return new CommandResult<Guid>(isSucceed, aggregationId, agg.Failures.ToImmutableList());
        }
    }
}