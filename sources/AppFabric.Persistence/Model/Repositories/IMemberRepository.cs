using AppFabric.Domain.BusinessObjects;
using DFlow.Persistence.Repositories;

namespace AppFabric.Persistence.Model.Repositories
{
    public interface IMemberRepository : IRepository<MemberState, Member>
    {
        Member Get(EntityId entityId);
    }
}