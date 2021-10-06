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
using AppFabric.Business.CommandHandlers.ExtensionMethods;
using AppFabric.Business.Framework;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;
using System;
using AppFabric.Domain.AggregationBilling;
using System.Linq;

namespace AppFabric.Business.CommandHandlers
{
    public class CreateBillingCommandHandler : CommandHandler<CreateBillingCommand, CommandResult<Guid>>
    {
        private readonly IDbSession<IBillingRepository> _dbSession;
        private readonly ILogger<CreateBillingCommandHandler> _logger;

        public CreateBillingCommandHandler(ILogger<CreateBillingCommandHandler> logger, IMediator publisher, IDbSession<IBillingRepository> dbSession)
            : base(logger, publisher)
        {
            _dbSession = dbSession;
            _logger = logger;
        }

        protected override CommandResult<Guid> ExecuteCommand(CreateBillingCommand command)
        {
            var isSucceed = false;
            var aggregationId = Guid.Empty;

            _logger.LogDebug("Criada agregação a partir do comando {CommandName} com valores {Valores}",
                nameof(command), command);

            var agg = BillingAggregationRoot.CreateFrom(EntityId2.From(command.Id));

            if (!agg.Failures.Any())
            {
                using (_logger.BeginScope("Persistencia"))
                {
                    _dbSession.Repository.Add(agg.GetChange());
                    _dbSession.SaveChanges();
                }

                using (_logger.BeginScope("Publicacão de Eventos"))
                {
                    agg.GetEvents().ToImmutableList().ForEach(ev => Publisher.Publish(ev));
                }

                isSucceed = true;
                aggregationId = agg.GetChange().Identity.Value;
            }

            return new CommandResult<Guid>(isSucceed, aggregationId, agg.Failures.ToImmutableList());
        }
    }
}
