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
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.CommandHandlers.ExtensionMethods;
using AppFabric.Business.CommandHandlers.Factories;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Model.Repositories;
using DFlow.Business.Cqrs;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Persistence;

namespace AppFabric.Business.CommandHandlers
{
    public sealed class UpdateProjectCommandHandler : CommandHandler<UpdateProjectCommand, ExecutionResult>
    {
        private readonly IDbSession<IProjectRepository> _dbSession;
        private readonly IAggregateFactory<ProjectAggregationRoot, Project> _factory;

        public UpdateProjectCommandHandler(
            IDomainEventBus publisher, 
            IDbSession<IProjectRepository> dbSession, 
            IAggregateFactory<ProjectAggregationRoot, Project> factory)
            :base(publisher)
        {
            _dbSession = dbSession;
            _factory = factory;
        }
        
        protected override async Task<ExecutionResult> ExecuteCommand(
            UpdateProjectCommand command, 
            CancellationToken cancellationToken)
        {
            var project = _dbSession.Repository.Get(EntityId.From(command.Id));
            var agg = _factory.Create(project);

            agg.UpdateDetail(command.ToProjectDetail(), null);

            var isSucceed = false;
      
            if (agg.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                await _dbSession.SaveChangesAsync(cancellationToken);
                
                agg.GetEvents().ToImmutableList()
                    .ForEach( ev => Publisher.Publish(ev));
                isSucceed = true;
            }
            
            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}