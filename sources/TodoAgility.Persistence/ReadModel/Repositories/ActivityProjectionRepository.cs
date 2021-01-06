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
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Persistence.ReadModel.Projections;
using TodoAgility.Persistence.ReadModel.Repositories;

namespace TodoAgility.Agile.Persistence.Repositories
{
    public sealed class ActivityProjectionRepository : IActivityProjectionRepository
    {
        private TodoAgilityProjectionsDbContext Context { get; }
        public ActivityProjectionRepository(TodoAgilityProjectionsDbContext context)
        {
            Context = context;
        }

        public ActivityProjection Get(IExposeValue<uint> id)
        {
            return Context.Activities.FindOne(ac => ac.ActivityId == id.GetValue());
        }

        public void Add(ActivityProjection entity)
        {
            Context.Activities.Upsert(entity);
        }

        public void Remove(ActivityProjection entity)
        {
            Context.Activities.Delete( new BsonValue(BitConverter.GetBytes(entity.ActivityId)));
        }

        public IEnumerable<ActivityProjection> Find(Expression<Func<ActivityProjection, bool>> predicate)
        {
            return Context.Activities.Query().Where(predicate).ToList();
        }
    }
}