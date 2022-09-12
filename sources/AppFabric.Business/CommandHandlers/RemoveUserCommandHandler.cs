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
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Model.Repositories;
using DFlow.Business.Cqrs;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Persistence;

namespace AppFabric.Business.CommandHandlers
{
    public sealed class RemoveUserCommandHandler : CommandHandler<RemoveUserCommand, ExecutionResult>
    {
        private readonly IAggregateFactory<UserAggregationRoot, User> _factory;
        private readonly IDbSession<IUserRepository> _userDb;

        public RemoveUserCommandHandler(
            IDomainEventBus publisher,
            IDbSession<IUserRepository> userDb,
            IAggregateFactory<UserAggregationRoot, User> factory)
            : base(publisher)
        {
            _userDb = userDb;
            _factory = factory;
        }

        protected override async Task<ExecutionResult> ExecuteCommand(
            RemoveUserCommand command,
            CancellationToken cancellationToken)
        {
            var user = _userDb.Repository.Get(command.Id);


            // user associado a um projeto não pode ser removido
            // no caso precisamos de uma pesquisa ou uma flag, acho 
            // que uma flag mantida pela associação aos projetos mais interessante
            // acho uma técnica mais "event-based" consistent dentro do modelo
            // que estamos desenvolvendo
            var agg = _factory.Create(user);
            // agg.Remove(null);

            var isSucceed = false;

            if (agg.IsValid)
            {
                await _userDb.Repository.Remove(agg.GetChange());
                await _userDb.SaveChangesAsync(cancellationToken);

                agg.GetEvents().ToImmutableList()
                    .ForEach(ev =>
                        Publisher.Publish(ev, cancellationToken));
                isSucceed = true;
            }

            return new ExecutionResult(isSucceed, agg.Failures);
        }
    }
}