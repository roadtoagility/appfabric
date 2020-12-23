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
using TodoAgility.Agile.Domain.AggregationProject;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public ProjectRepository(DbContext context)
        {
            DbContext = context as ProjectDbContext;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private ProjectDbContext DbContext { get; }

        // https://docs.microsoft.com/en-us/ef/core/saving/disconnected-entities

        public void Add(IExposeValue<ProjectState> entity)
        {
            var entry = entity.GetValue();
            var oldState =
                DbContext.Projects.Include(b => b.Activities)
                    .FirstOrDefault(b => b.ProjectId == entry.ProjectId);

            if (oldState == null)
            {
                DbContext.Projects.Add(entry);
            }
            else
            {
                DbContext.Entry(oldState).CurrentValues.SetValues(entry);
                foreach (var activity in entry.Activities)
                {
                    var existing = oldState.Activities.AsQueryable()
                        .FirstOrDefault(p => p.ActivityReferenceId == activity.ActivityReferenceId);

                    if (existing == null)
                    {
                        oldState.Activities.Add(activity);
                    }
                    else
                    {
                        DbContext.Entry(oldState).CurrentValues.SetValues(activity);
                    }
                }

                foreach (var activity in oldState.Activities)
                {
                    if (!entry.Activities.All(p => p.ActivityReferenceId == activity.ActivityReferenceId))
                    {
                        DbContext.Remove(activity);
                    }                    
                }
            }
        }

        public void Remove(IExposeValue<ProjectState> entity)
        {
            DbContext.Entry(entity.GetValue()).State = EntityState.Deleted;
        }

        public Project Get(EntityId id)
        {
            IExposeValue<uint> entityId = id;
            var project = DbContext.Projects.AsNoTracking()
                .Include(b => b.Activities)
                .OrderByDescending(ob => ob.ProjectId)
                .First(t => t.ProjectId == entityId.GetValue());

            return Project.FromState(project);
        }

        public IEnumerable<Project> Find(Expression<Func<ProjectState, bool>> predicate)
        {
            return DbContext.Projects.Where(predicate).AsNoTracking()
                .Select(t => Project.FromState(t));
        }
    }
}