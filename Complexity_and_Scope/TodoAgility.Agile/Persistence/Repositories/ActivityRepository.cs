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
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories
{
    public sealed class ActivityRepository : IActivityRepository
    {
        public ActivityRepository(DbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            DbContext = context as ActivityDbContext;
        }

        private ActivityDbContext DbContext { get; }

        // https://docs.microsoft.com/en-us/ef/core/saving/disconnected-entities

        public void AddProject(IExposeValue<ProjectStateReference> project)
        {
            var entry = project.GetValue();
            var oldState = DbContext.Projects.FirstOrDefault(b => b.ProjectId == entry.ProjectId);

            if (oldState == null)
            {
                DbContext.Projects.Add(entry);
            }
            else
            {
                DbContext.Entry(oldState).CurrentValues.SetValues(entry);
            }
        }
        
        public void Add(IExposeValue<ActivityState> entity)
        {
            var entry = entity.GetValue();
            var oldState = DbContext.Activities.FirstOrDefault(b => b.ActivityId == entry.ActivityId);

            if (oldState == null)
            {
                DbContext.Activities.Add(entry);
            }
            else
            {
                DbContext.Entry(oldState).CurrentValues.SetValues(entry);
            }
        }

        public void Remove(IExposeValue<ActivityState> entity)
        {
            DbContext.Activities.Remove(entity.GetValue());
        }
        
        public void RemoveProject(IExposeValue<ProjectStateReference> entity)
        {
            DbContext.Projects.Remove(entity.GetValue());
        }

        public Activity Get(EntityId id)
        {
            IExposeValue<uint> entityId = id;
            var task = DbContext.Activities.AsQueryable()
                .OrderByDescending(ob => ob.ActivityId)
                .First(t => t.ActivityId == entityId.GetValue());

            return Activity.FromState(task);
        }

        public Project GetProject(EntityId id)
        {
            IExposeValue<uint> entityId = id;
            var found = DbContext.Projects.AsQueryable()
                .OrderByDescending(ob => ob.ProjectId)
                .First(t => t.ProjectId == entityId.GetValue());

            return Project.FromState(found);
        }

        public IEnumerable<Activity> Find(Expression<Func<ActivityState, bool>> predicate)
        {
            return DbContext.Activities.Where(predicate).Select(t => Activity.FromState(t));
        }
    }
}