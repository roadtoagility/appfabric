using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Persistence.Model.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        public ActivityRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        private AppFabricDbContext DbContext { get; }

        public Activity Get(EntityId entityId)
        {
            throw new NotImplementedException();
        }

        public void Add(Activity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Activity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Activity> Find(Expression<Func<ActivityState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> FindAsync(Expression<Func<ActivityState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> FindAsync(Expression<Func<ActivityState, bool>> predicate,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}