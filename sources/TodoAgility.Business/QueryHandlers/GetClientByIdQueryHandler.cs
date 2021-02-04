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

using System;
using TodoAgility.Business.Framework;
using TodoAgility.Business.QueryHandlers.Filters;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Framework.ReadModel.Repositories;
using TodoAgility.Persistence.ReadModel.Repositories;

namespace TodoAgility.Business.QueryHandlers
{
    public sealed class GetClientByIdQueryHandler : QueryHandler<GetClientByIdFilter, GetClientResponse>
    {
        private readonly IDbSession<IUserProjectionRepository> _dbSession;

        public GetClientByIdQueryHandler(IDbSession<IUserProjectionRepository> session)
        {
            _dbSession = session;
        }

        protected override GetClientResponse ExecuteQuery(GetClientByIdFilter filter)
        {
            //we need a validation like a commandhandler here
            
            var user = _dbSession.Repository
                .Get(EntityId.From(filter.Id));
           
            return GetClientResponse.From(true, user);
        }
    }
}