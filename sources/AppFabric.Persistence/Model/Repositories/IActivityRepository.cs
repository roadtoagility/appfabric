using AppFabric.Domain.BusinessObjects;
using DFlow.Persistence.Repositories;

namespace AppFabric.Persistence.Model.Repositories
{
    public interface IActivityRepository : IRepository<ActivityState, Activity>
    {
        Activity Get(EntityId entityId);
    }
}