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
    public class ReleaseRepository : IReleaseRepository
    {
        private AppFabricDbContext DbContext { get; }

        public ReleaseRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        public void Add(Release entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Release> Find(Expression<Func<ReleaseState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Release entity)
        {
            throw new NotImplementedException(); 
        }

        public Release Get(EntityId id)
        {
            throw new NotImplementedException();
        }
    }
}
