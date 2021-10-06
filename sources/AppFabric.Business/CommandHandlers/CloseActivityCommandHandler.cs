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
using AppFabric.Domain.AggregationActivity;
using System.Linq;

namespace AppFabric.Business.CommandHandlers
{
    public class CloseActivityCommandHandler : CommandHandler<CloseActivityCommand, ExecutionResult>
    {
        private readonly IDbSession<IActivityRepository> _dbSession;
        private readonly ILogger<CloseActivityCommandHandler> _logger;

        public CloseActivityCommandHandler(ILogger<CloseActivityCommandHandler> logger, IMediator publisher, IDbSession<IActivityRepository> dbSession)
            : base(logger, publisher)
        {
            _dbSession = dbSession;
            _logger = logger;
        }

        protected override ExecutionResult ExecuteCommand(CloseActivityCommand command)
        {
            var activity = _dbSession.Repository.Get(EntityId.From(command.Id));
            var agg = ActivityAggregationRoot.ReconstructFrom(activity);
            var isSucceed = false;

            if (!agg.Failures.Any())
            {
                agg.Close();

                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                agg.GetEvents().ToImmutableList().ForEach(ev => Publisher.Publish(ev));
                isSucceed = true;
            }

            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}
