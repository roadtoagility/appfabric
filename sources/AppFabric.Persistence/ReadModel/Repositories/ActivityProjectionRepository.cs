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
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Persistence.ReadModel.Repositories
{
    public sealed class ActivityProjectionRepository : IActivityProjectionRepository
    {
        public ActivityProjectionRepository(AppFabricDbContext context)
        {
            Context = context;
        }

        private AppFabricDbContext Context { get; }


        public Task Add(ActivityProjection entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(ActivityProjection entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActivityProjection> Find(Expression<Func<ActivityProjection, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityProjection>> FindAsync(Expression<Func<ActivityProjection, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ActivityProjection>> FindAsync(Expression<Func<ActivityProjection, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}