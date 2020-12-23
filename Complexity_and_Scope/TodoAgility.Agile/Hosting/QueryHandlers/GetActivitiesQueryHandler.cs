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

using TodoAgility.Agile.CQRS.CommandHandlers;
using TodoAgility.Agile.CQRS.Framework;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Hosting.Framework;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.CQRS.QueryHandlers
{
    public sealed class GetActivitiesQueryHandler : QueryHandler<GetActivitiesFilter, GetActivitiesResponse>
    {
        private readonly IDbSession<IActivityProjectionRepository> _activitySession;

        public GetActivitiesQueryHandler(IDbSession<IActivityProjectionRepository> activitySession)
        {
            _activitySession = activitySession;
        }

        protected override GetActivitiesResponse ExecuteQuery(GetActivitiesFilter filter)
        {
            IExposeValue<uint> id = filter.ProjectId;
            var activities = _activitySession.Repository
                .Find(fl => fl.ProjectId == id.GetValue());
           
            return GetActivitiesResponse.From(true, activities);
        }
    }
}