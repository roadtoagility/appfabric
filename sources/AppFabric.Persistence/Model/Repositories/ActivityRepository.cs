using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Persistence.Model.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private AppFabricDbContext DbContext { get; }

        public ActivityRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        public Activity Get(EntityId id)
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
    }
}
