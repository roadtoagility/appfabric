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
using TodoAgility.Domain.BusinessObjects;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Persistence.ExtensionMethods;
using TodoAgility.Persistence.Model;


namespace TodoAgility.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public ProjectRepository(DbContext context)
        {
            DbContext = context as TodoAgilityDbContext;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private TodoAgilityDbContext DbContext { get; }

        // https://docs.microsoft.com/en-us/ef/core/saving/disconnected-entities

        public void Add(Project entity)
        {
            var entry = entity.ToProjectState();
            var oldState =
                DbContext.Projects.FirstOrDefault(b => b.Code == entry.Code);

            if (oldState == null)
            {
                DbContext.Projects.Add(entry);
            }
            else
            {
                DbContext.Entry(oldState).CurrentValues.SetValues(entry);
            }
        }

        public void Remove(Project entity)
        {
            var entry = entity.ToProjectState();

            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        public Project Get(ProjectCode code)
        {
            var project = DbContext.Projects.AsNoTracking()
                .OrderByDescending(ob => ob.Code)
                .First(t => t.Code == code.Value);
            return project.ToProject();
        }

        public IEnumerable<Project> Find(Expression<Func<ProjectState, bool>> predicate)
        {
            return DbContext.Projects.Where(predicate).AsNoTracking()
                .Select(t =>  t.ToProject());
            ;
        }
    }
}