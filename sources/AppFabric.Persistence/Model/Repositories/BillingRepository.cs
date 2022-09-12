using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Persistence.Model.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        public BillingRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        private AppFabricDbContext DbContext { get; }

        public Task Add(Billing entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Billing entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Billing> Find(Expression<Func<BillingState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Billing>> FindAsync(Expression<Func<BillingState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Billing>> FindAsync(Expression<Func<BillingState, bool>> predicate,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Billing Get(EntityId id)
        {
            throw new NotImplementedException();
        }
    }
}