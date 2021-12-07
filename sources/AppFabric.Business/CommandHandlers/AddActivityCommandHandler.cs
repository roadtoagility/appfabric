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
using AppFabric.Business.Framework;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AppFabric.Business.CommandHandlers
{
    public class AddActivityCommandHandler : CommandHandler<AddActivityCommand, ExecutionResult>
    {
        private readonly IDbSession<IReleaseRepository> _dbSession;
        private readonly IDbSession<IActivityRepository> _dbActivitySession;
        private readonly ILogger<AddActivityCommandHandler> _logger;
        private readonly AggregateFactory _factory;

        public AddActivityCommandHandler(ILogger<AddActivityCommandHandler> logger, 
            IMediator publisher, 
            IDbSession<IReleaseRepository> dbSession, 
            IDbSession<IActivityRepository> dbActivitySession,
            AggregateFactory factory)
            : base(logger, publisher)
        {
            _dbSession = dbSession;
            _dbActivitySession = dbActivitySession;
            _logger = logger;
            _factory = factory;
        }

        protected override ExecutionResult ExecuteCommand(AddActivityCommand command)
        {
            var release = _dbSession.Repository.Get(EntityId.From(command.Id));
            var agg = _factory.Create(new LoadReleaseCommand(release));
            var isSucceed = false;

            if (!agg.Failures.Any())
            {
                var activity = _dbActivitySession.Repository.Get(EntityId.From(command.ActivityId));
                agg.AddActivity(activity);

                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                agg.GetEvents().ToImmutableList().ForEach(ev => Publisher.Publish(ev));
                isSucceed = true;
            }

            return new ExecutionResult(isSucceed, agg.Failures.ToImmutableList());
        }
    }
}
