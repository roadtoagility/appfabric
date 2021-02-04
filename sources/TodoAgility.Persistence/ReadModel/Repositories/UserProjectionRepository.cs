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
using System.Linq;
using System.Linq.Expressions;
using LiteDB;
using TodoAgility.Domain.Framework.BusinessObjects;

namespace TodoAgility.Persistence.ReadModel.Repositories
{
    public sealed class UserProjectionRepository : IUserProjectionRepository
    {
        private readonly TodoAgilityDbContext _context;
        public UserProjectionRepository(TodoAgilityDbContext context)
        {
            _context = context;
        }

        public UserProjection Get(EntityId id)
        {
            var user = _context.UsersProjection.FirstOrDefault(ac => ac.Id == id.Value);
            
            if (user == null)
            {
                UserProjection.Empty();
            }
            
            return user;
        }

        public void Add(UserProjection entity)
        {
            var oldState =
                _context.UsersProjection.FirstOrDefault(b => b.Id == entity.Id);

            if (oldState == null)
            {
                _context.UsersProjection.Add(entity);
            }
            else
            {
                _context.Entry(oldState).CurrentValues.SetValues(entity);
            }
        }

        public void Remove(UserProjection entity)
        {
            _context.UsersProjection.Remove(entity);
        }

        public IReadOnlyList<UserProjection> Find(Expression<Func<UserProjection, bool>> predicate)
        {
            return _context.UsersProjection.Where(predicate).ToList();
        }
    }
}