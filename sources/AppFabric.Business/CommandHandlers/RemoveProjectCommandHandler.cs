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
using FluentMediator;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.CommandHandlers.Factories;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Model.Repositories;
using DFlow.Business.Cqrs;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Persistence;
using Microsoft.Extensions.Logging;

namespace AppFabric.Business.CommandHandlers
{
    public sealed class RemoveProjectCommandHandler : CommandHandler<RemoveProjectCommand, ExecutionResult>
    {
        private readonly IDbSession<IProjectRepository> _projectDb;
        private readonly IDbSession<IUserRepository> _userDb;
        private readonly IAggregateFactory<ProjectAggregationRoot, Project> _factory;
        
        public RemoveProjectCommandHandler(IDomainEventBus publisher, IDbSession<IProjectRepository> projectDb,
            IDbSession<IUserRepository> userDb, IAggregateFactory<ProjectAggregationRoot, Project> factory)
            :base(publisher)
        {
            _projectDb = projectDb;
            _userDb = userDb;
            _factory = factory;
        }
        
        protected override async Task<ExecutionResult> ExecuteCommand(
            RemoveProjectCommand command, 
            CancellationToken cancellationToken)
        {
            var isSucceed = false;

            var project = _projectDb.Repository.Get(command.Id);
            
            var agg = _factory.Create(project);
            agg.Remove(null);

            if (agg.IsValid)
            {
                _projectDb.Repository.Remove(agg.GetChange());
                await _projectDb.SaveChangesAsync(cancellationToken);
                
                agg.GetEvents().ToImmutableList()
                    .ForEach( ev => 
                        Publisher.Publish(ev, cancellationToken));
                isSucceed = true;
            }
            
            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}