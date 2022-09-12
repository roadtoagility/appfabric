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
using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationActivity.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Model.Repositories;
using DFlow.Business.Cqrs;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Persistence;

namespace AppFabric.Business.CommandHandlers
{
    public class AssignResponsibleCommandHandler : CommandHandler<AssignResponsibleCommand, ExecutionResult>
    {
        private readonly IDbSession<IMemberRepository> _dbMemberSession;
        private readonly IDbSession<IActivityRepository> _dbSession;
        private readonly IAggregateFactory<ActivityAggregationRoot, Activity> _factory;

        public AssignResponsibleCommandHandler(IDomainEventBus publisher,
            IAggregateFactory<ActivityAggregationRoot, Activity> factory,
            IDbSession<IActivityRepository> dbSession,
            IDbSession<IMemberRepository> dbMemberSession)
            : base(publisher)
        {
            _factory = factory;
            _dbSession = dbSession;
            _dbMemberSession = dbMemberSession;
        }

        protected override async Task<ExecutionResult> ExecuteCommand(
            AssignResponsibleCommand command,
            CancellationToken cancellationToken)
        {
            var activity = _dbSession.Repository.Get(command.Id);
            var member = _dbMemberSession.Repository.Get(command.MemberId);

            var agg = _factory.Create(activity);
            agg.Assign(member, new ActivityResponsibleSpecification());

            var isSucceed = false;

            if (agg.IsValid)
            {
                await _dbSession.Repository.Add(agg.GetChange());
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