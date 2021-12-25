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

using System.Collections.Immutable;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Business.CommandHandlers.Factories;
using AppFabric.Domain.AggregationRelease;
using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Persistence;

namespace AppFabric.Business.CommandHandlers
{
    public class CreateReleaseCommandHandler : CommandHandler<CreateReleaseCommand, CommandResult<Guid>>
    {
        private readonly IDbSession<IReleaseRepository> _dbSession;
        private readonly IAggregateFactory<ReleaseAggregationRoot, CreateReleaseCommand> _factory;

        public CreateReleaseCommandHandler(
            IDomainEventBus publisher,
            IDbSession<IReleaseRepository> dbSession,
            IAggregateFactory<ReleaseAggregationRoot, CreateReleaseCommand> factory)
            : base(publisher)
        {
            _dbSession = dbSession;
            _factory = factory;
        }

        protected override async Task<CommandResult<Guid>> ExecuteCommand(
            CreateReleaseCommand command, 
            CancellationToken cancellationToken)
        {
            var isSucceed = false;
            var aggregationId = Guid.Empty;

            var agg = _factory.Create(command);

            if (agg.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                await _dbSession.SaveChangesAsync(cancellationToken);
                
                agg.GetEvents().ToImmutableList()
                    .ForEach(ev => 
                        Publisher.Publish(ev, cancellationToken));

                isSucceed = true;
                aggregationId = agg.GetChange().Identity.Value;
            }

            return new CommandResult<Guid>(isSucceed, aggregationId, agg.Failures.ToImmutableList());
        }
    }
}
