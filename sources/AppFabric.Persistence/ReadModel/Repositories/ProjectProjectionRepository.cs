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
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace AppFabric.Persistence.ReadModel.Repositories
{
    public sealed class ProjectProjectionRepository : IProjectProjectionRepository
    {
        private readonly AppFabricDbContext _context;

        public ProjectProjectionRepository(AppFabricDbContext context)
        {
            _context = context;
        }

        public ProjectProjection Get(EntityId id)
        {
            var project = _context.Set<ProjectProjection>()
                .FirstOrDefault(ac => ac.Id.Equals(id.Value));

            if (project == null) ProjectProjection.Empty();

            return project;
        }

        public async Task Add(ProjectProjection entity)
        {
            var cancel = new CancellationTokenSource();
            var oldState = await FindOne(b => b.Id == entity.Id, cancel.Token)
                .ConfigureAwait(false);

            if (oldState == null)
                _context.Set<ProjectProjection>().Add(entity);
            else
                _context.Entry(oldState).CurrentValues.SetValues(entity);
        }

        public Task Remove(ProjectProjection entity)
        {
            _context.Set<ProjectProjection>().Remove(entity);
            return Task.CompletedTask;
        }

        public IReadOnlyList<ProjectProjection> Find(Expression<Func<ProjectProjection, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ProjectProjection>> FindAsync(Expression<Func<ProjectProjection, bool>> predicate)
        {
            var cancellation = new CancellationTokenSource();
            return FindAsync(predicate, cancellation.Token);
        }

        public async Task<IReadOnlyList<ProjectProjection>> FindAsync(Expression<Func<ProjectProjection, bool>> predicate
            , CancellationToken cancellationToken)
        {
            return await _context.Set<ProjectProjection>().Where(predicate)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<ProjectProjection> FindOne(Expression<Func<ProjectProjection, bool>> predicate
            , CancellationToken cancellation)
        {
            return await FindAsync(predicate, cancellation)
                .ContinueWith(result => result.Result.First(),cancellation);
        }
    }
}