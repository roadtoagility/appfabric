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
using System.Threading;
using System.Threading.Tasks;
using FluentMediator;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.CommandHandlers.ExtensionMethods;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;
using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationActivity.Specifications;
using DFlow.Business.Cqrs;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Persistence;

namespace AppFabric.Business.CommandHandlers
{
    public class CloseActivityCommandHandler : CommandHandler<CloseActivityCommand, ExecutionResult>
    {
        private readonly IDbSession<IActivityRepository> _dbSession;
        private readonly IAggregateFactory<ActivityAggregationRoot, Activity> _factory;


        public CloseActivityCommandHandler(
            IDomainEventBus publisher, 
            IAggregateFactory<ActivityAggregationRoot, Activity> factory,
            IDbSession<IActivityRepository> dbSession)
            : base(publisher)
        {
            _factory = factory;
            _dbSession = dbSession;
        }

        protected override async Task<ExecutionResult> ExecuteCommand(
            CloseActivityCommand command, 
            CancellationToken cancellationToken )
        {
            //TODO: errado o ProjectId, deveria ser o ID da atividade
            
            var activity = _dbSession.Repository.Get(command.ActivityId);
            
            var agg = _factory.Create(activity);
            agg.Close(new ActivityCanBeClosed());
            
            // aqui seria um exemplo de uma regra mais complexa como condição para o fechamento da atividade
            //agg.Close(new ActivityCanBeClosed().And(new ActivityCanBeCLosedByMe()));

            var isSucceed = false;

            if (agg.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                await _dbSession.SaveChangesAsync(cancellationToken);
                
                agg.GetEvents().ToImmutableList().ForEach(ev => Publisher.Publish(ev,cancellationToken));
                isSucceed = true;
            }

            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}
