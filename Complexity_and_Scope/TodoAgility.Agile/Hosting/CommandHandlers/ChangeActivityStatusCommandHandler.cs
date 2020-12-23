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
using TodoAgility.Agile.CQRS.CommandHandlers;
using TodoAgility.Agile.CQRS.Framework;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Hosting.Framework;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.Hosting.CommandHandlers
{
    public sealed class ChangeActivityStatusCommandHandler : CommandHandler<ChangeActivityStatusCommand, ExecutionResult>
    {
        private readonly IDbSession<IActivityRepository> _session;

        public ChangeActivityStatusCommandHandler(IDbSession<IActivityRepository> session, IEventDispatcher publisher)
        :base(publisher)
        {
            _session = session;
        }

        protected override ExecutionResult ExecuteCommand(ChangeActivityStatusCommand command)
        {
            var currentState = _session.Repository.Get(command.Id);
            var agg = ActivityAggregationRoot.ReconstructFrom(currentState);

            agg.ChangeTaskStatus(command.NewStatus);
            var task = agg.GetChange();
            var isSucceed = false;
            
            if (agg.ValidationResults.IsValid)
            {
                _session.Repository.Add(task);
                _session.SaveChanges();
                Publisher.Publish(agg.GetEvents());
                isSucceed = true;
            }
            
            return new ExecutionResult(isSucceed, agg.ValidationResults.Errors.ToImmutableList());
        }
    }
}