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
using System.Threading;
using System.Threading.Tasks;
using DFlow.Business.Cqrs;
using DFlow.Domain.Events;
using DFlow.Persistence;

namespace AppFabric.Business.CommandHandlers
{
    public class AddReleaseCommandHandler : CommandHandler<AddReleaseCommand, ExecutionResult>
    {
        private readonly IDbSession<IBillingRepository> _dbSession;
        private readonly IDbSession<IReleaseRepository> _dbReleaseSession;
        private readonly AggregateFactory _factory;

        public AddReleaseCommandHandler(ILogger<AddReleaseCommandHandler> logger, 
            IDomainEventBus publisher, IDbSession<IBillingRepository> dbSession, 
            IDbSession<IReleaseRepository> dbReleaseSession,
            AggregateFactory factory)
            : base(publisher)
        {
            _dbSession = dbSession;
            _dbReleaseSession = dbReleaseSession;
            _factory = factory;
        }

        protected override async Task<ExecutionResult> ExecuteCommand(
            AddReleaseCommand command, 
            CancellationToken cancellationToken)
        {
            var isSucceed = false;

            var billing = _dbSession.Repository.Get(command.Id);
            var release = _dbReleaseSession.Repository.Get(command.ReleaseId);
            
            var agg = _factory.Create(billing);
            agg.AddRelease(release, null);
            
            if (!agg.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                await _dbSession.SaveChangesAsync(cancellationToken);
                
                agg.GetEvents().ToImmutableList()
                    .ForEach(ev => Publisher.Publish(ev));
                
                isSucceed = true;
            }

            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}
