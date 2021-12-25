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
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.AggregationRelease;
using AppFabric.Domain.BusinessObjects;
using DFlow.Business.Cqrs;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Domain.Specifications;
using DFlow.Persistence;

namespace AppFabric.Business.CommandHandlers
{
    public class AddActivityCommandHandler : CommandHandler<AddActivityCommand, ExecutionResult>
    {
        private readonly IDbSession<IReleaseRepository> _dbSession;
        private readonly IDbSession<IActivityRepository> _dbActivitySession;
        private readonly ISpecification<Activity> _spec;
        private readonly IAggregateFactory<ReleaseAggregationRoot, Release> _factory;

        public AddActivityCommandHandler(
            IDomainEventBus publisher, 
            IDbSession<IReleaseRepository> dbSession, 
            IDbSession<IActivityRepository> dbActivitySession,
            ISpecification<Activity> spec,
            IAggregateFactory<ReleaseAggregationRoot, Release> factory)
            : base(publisher)
        {
            _spec = spec;
            _dbSession = dbSession;
            _dbActivitySession = dbActivitySession;
            _factory = factory;
        }

        protected override async Task<ExecutionResult> ExecuteCommand(
            AddActivityCommand command, 
            CancellationToken cancellationToken)
        {
            // pré-requisitos
            var release = _dbSession.Repository.Get(command.Id);
            var activity = _dbActivitySession.Repository.Get(command.ActivityId);
            
            //criando agregação com Specificação passada para o factory
            var agg = _factory.Create(release);
            
            //referenciando outra agregação com base nas regras da especificação
            agg.AddActivity(activity, _spec);

            var isSucceed = false;

            if (agg.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                await _dbSession.SaveChangesAsync(cancellationToken);
                
                agg.GetEvents().ToImmutableList()
                    .ForEach(ev => 
                        Publisher.Publish(ev, cancellationToken));
                isSucceed = true;
            }

            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}
