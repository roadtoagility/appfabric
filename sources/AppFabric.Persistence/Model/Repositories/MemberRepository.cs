using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Persistence.Model.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public MemberRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        private AppFabricDbContext DbContext { get; }

        public void Add(Member entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Member entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> Find(Expression<Func<MemberState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Member>> FindAsync(Expression<Func<MemberState, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Member>> FindAsync(Expression<Func<MemberState, bool>> predicate,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Member Get(EntityId id)
        {
            throw new NotImplementedException();
        }
    }
}