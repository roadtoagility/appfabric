using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Persistence.Model.Repositories
{
    public class ReleaseRepository : IReleaseRepository
    {
        public ReleaseRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        private AppFabricDbContext DbContext { get; }

        public Task Add(Release entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Release> Find(Expression<Func<ReleaseState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Release>> FindAsync(Expression<Func<ReleaseState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Release>> FindAsync(Expression<Func<ReleaseState, bool>> predicate,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Release entity)
        {
            throw new NotImplementedException();
        }

        public Release Get(EntityId id)
        {
            throw new NotImplementedException();
        }
    }
}