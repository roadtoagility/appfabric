﻿// Copyright (C) 2020  Road to Agility
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
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.CommandHandlers.ExtensionMethods;
using TodoAgility.Business.Framework;
using TodoAgility.Domain.AggregationProject;
using TodoAgility.Domain.BusinessObjects;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Model.Repositories;

namespace TodoAgility.Business.CommandHandlers
{
    public sealed class UpdateProjectCommandHandler : CommandHandler<UpdateProjectCommand, ExecutionResult>
    {
        private readonly IDbSession<IProjectRepository> _dbSession;
        
        public UpdateProjectCommandHandler(IMediator publisher, IDbSession<IProjectRepository> dbSession)
            :base(publisher)
        {
            _dbSession = dbSession;
        }
        
        protected override ExecutionResult ExecuteCommand(UpdateProjectCommand command)
        {
            var project = _dbSession.Repository.Get(EntityId.From(command.Id));

            var detail = command.ToProjectDetail();
            var agg = ProjectAggregationRoot.ReconstructFrom(project);

            agg.UpdateDetail(command.ToProjectDetail());
            
            var isSucceed = false;
      
            if (agg.ValidationResults.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                agg.GetEvents().ToImmutableList().ForEach( ev => Publisher.Publish(ev));
                isSucceed = true;
            }
            
            return new ExecutionResult(isSucceed, agg.ValidationResults.Errors.ToImmutableList());
        }
    }
}