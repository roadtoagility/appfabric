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
    public sealed class ProjectProjectionRepository : IProjectProjectionRepository
    {
        private readonly TodoAgilityProjectionsDbContext _context;
        public ProjectProjectionRepository(TodoAgilityProjectionsDbContext context)
        {
            _context = context;
        }

        public ProjectProjection Get(EntityId id)
        {
            return _context.Projects.FindOne(ac => ac.Id == id.Value);
        }

        public void Add(ProjectProjection entity)
        {
            _context.Projects.Upsert(entity);
        }

        public void Remove(ProjectProjection entity)
        {
            _context.Projects.Delete( new BsonValue(BitConverter.GetBytes(entity.Id)));
        }

        public IReadOnlyList<ProjectProjection> Find(Expression<Func<ProjectProjection, bool>> predicate)
        {
            return _context.Projects.Query().Where(predicate).ToList();
        }
    }
}