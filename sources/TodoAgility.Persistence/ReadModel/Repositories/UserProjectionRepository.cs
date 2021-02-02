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
using LiteDB;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Persistence.ReadModel.Projections;

namespace TodoAgility.Persistence.ReadModel.Repositories
{
    public sealed class UserProjectionRepository : IUserProjectionRepository
    {
        private readonly TodoAgilityProjectionsDbContext _context;
        public UserProjectionRepository(TodoAgilityProjectionsDbContext context)
        {
            _context = context;
        }

        public UserProjection Get(EntityId id)
        {
            return _context.Users.FindOne(ac => ac.Id == id.Value);
        }

        public void Add(UserProjection entity)
        {
            _context.Users.Upsert(entity);
        }

        public void Remove(UserProjection entity)
        {
            var isExecuted = _context.Users.Delete(new BsonValue(entity.Id));

            _context.Database.Commit();
        }

        public IReadOnlyList<UserProjection> Find(Expression<Func<UserProjection, bool>> predicate)
        {
            return _context.Users.Query().Where(predicate).ToList();
        }
    }
}