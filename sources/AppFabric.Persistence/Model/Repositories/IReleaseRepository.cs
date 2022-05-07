using AppFabric.Domain.BusinessObjects;
using DFlow.Persistence.Repositories;

namespace AppFabric.Persistence.Model.Repositories
{
    public interface IReleaseRepository : IRepository<ReleaseState, Release>
    {
        Release Get(EntityId entityId);
    }
}