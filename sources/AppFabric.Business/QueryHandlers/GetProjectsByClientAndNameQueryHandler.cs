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
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers.Filters;
using AppFabric.Persistence.ReadModel;
using AppFabric.Persistence.ReadModel.Repositories;
using DFlow.Persistence;

namespace AppFabric.Business.QueryHandlers
{
    public sealed class
        GetProjectsByClientAndNameQueryHandler : QueryHandler<GetProjectsByClientAndNameFilter, GetProjectsResponse>
    {
        private readonly IDbSession<IProjectProjectionRepository> _dbSession;

        public GetProjectsByClientAndNameQueryHandler(IDbSession<IProjectProjectionRepository> session)
        {
            _dbSession = session;
        }

        protected override GetProjectsResponse ExecuteQuery(GetProjectsByClientAndNameFilter filter)
        {
            //we need a validation like a commandhandler here

            // var projects = _dbSession.Repository
            //     .FindAsync(p => p.ClientId.Equals(filter.ClientId.Value)
            //                && p.Name.Contains(filter.Name.Value));

            return GetProjectsResponse.From(true, ImmutableArray<ProjectProjection>.Empty);
        }
    }
}