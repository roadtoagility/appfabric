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
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.ExtensionMethods;
using DFlow.Domain.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace AppFabric.Persistence.Model.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public ProjectRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        private AppFabricDbContext DbContext { get; }

        // https://docs.microsoft.com/en-us/ef/core/saving/disconnected-entities

        public Task Add(Project entity)
        {
            var entry = entity.ToProjectState();
            var oldState = DbContext.Set<ProjectState>()
                .OrderByDescending(ob => ob.Id)
                .ThenByDescending(ob => ob.RowVersion)
                .FirstOrDefault(t => t.Id == entity.Identity.Value);

            if (oldState == null)
            {
                DbContext.Set<ProjectState>().Add(entry);
            }
            else
            {
                var version = VersionId.From(BitConverter.ToInt32(oldState.RowVersion));

                if (VersionId.Next(version) > entity.Version)
                    throw new DbUpdateConcurrencyException("This version is not the most updated for this object.");

                DbContext.Entry(oldState).CurrentValues.SetValues(entry);
            }
            return Task.CompletedTask;
        }

        public Task Remove(Project entity)
        {
            var oldState = Get(entity.Identity);

            if (VersionId.Next(oldState.Version) > entity.Version)
                throw new DbUpdateConcurrencyException("This version is not the most updated for this object.");

            var entry = entity.ToProjectState();

            DbContext.Set<ProjectState>().Remove(entry);
            return Task.CompletedTask;
        }

        public Project Get(EntityId id)
        {
            var project = DbContext.Set<ProjectState>().AsNoTracking()
                .OrderByDescending(ob => ob.Id)
                .ThenByDescending(ob => ob.RowVersion)
                .FirstOrDefault(t => t.Id == id.Value);

            if (project == null) return Project.Empty();

            return project.ToProject();
        }

        public IReadOnlyList<Project> Find(Expression<Func<ProjectState, bool>> predicate)
        {
            return DbContext.Set<ProjectState>().Where(predicate).AsNoTracking()
                .Select(t => t.ToProject()).ToImmutableList();
            ;
        }

        public Task<IReadOnlyList<Project>> FindAsync(Expression<Func<ProjectState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Project>> FindAsync(Expression<Func<ProjectState, bool>> predicate,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}