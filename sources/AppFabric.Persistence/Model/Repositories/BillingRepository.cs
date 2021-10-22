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
    public class BillingRepository : IBillingRepository
    {
        private AppFabricDbContext DbContext { get; }

        public BillingRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        public Billing Get(EntityId id)
        {
            throw new NotImplementedException();
        }

        public void Add(Billing entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Billing entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Billing> Find(Expression<Func<BillingState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Billing Get(EntityId2 id)
        {
            throw new NotImplementedException();
        }
    }
}
