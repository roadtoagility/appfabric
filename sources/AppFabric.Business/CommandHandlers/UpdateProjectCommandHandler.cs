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

namespace AppFabric.Business.CommandHandlers
{
    public sealed class UpdateProjectCommandHandler : CommandHandler<UpdateProjectCommand, ExecutionResult>
    {
        private readonly IDbSession<IProjectRepository> _dbSession;
        private readonly ILogger<UpdateProjectCommandHandler> _logger;
        
        public UpdateProjectCommandHandler(ILogger<UpdateProjectCommandHandler> logger, IMediator publisher, IDbSession<IProjectRepository> dbSession)
            :base(logger,publisher)
        {
            _dbSession = dbSession;
            _logger = logger;
        }
        
        protected override ExecutionResult ExecuteCommand(UpdateProjectCommand command)
        {
            var project = _dbSession.Repository.Get(EntityId.From(command.Id));
            var agg = ProjectAggregationRoot.ReconstructFrom(project);
            var isSucceed = false;
      
            if (agg.ValidationResults.IsValid)
            {
                agg.UpdateDetail(command.ToProjectDetail()); // dado inválido
                
                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                agg.GetEvents().ToImmutableList().ForEach( ev => Publisher.Publish(ev));
                isSucceed = true;
            }
            
            return new ExecutionResult(isSucceed, agg.ValidationResults.Errors.ToImmutableList());
        }
    }
}