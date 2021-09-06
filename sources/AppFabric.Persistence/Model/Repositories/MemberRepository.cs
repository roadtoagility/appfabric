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
    public class MemberRepository : IMemberRepository
    {
        private AppFabricDbContext DbContext { get; }

        public MemberRepository(AppFabricDbContext context)
        {
            DbContext = context;
        }

        public Member Get(EntityId id)
        {
            throw new NotImplementedException();
        }

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
    }
}
