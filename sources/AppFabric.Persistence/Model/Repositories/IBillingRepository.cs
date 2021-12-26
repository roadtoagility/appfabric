using AppFabric.Domain.BusinessObjects;
using DFlow.Persistence.Repositories;

namespace AppFabric.Persistence.Model.Repositories
{
    public interface IBillingRepository : IRepository<BillingState, Billing>
    {
        Billing Get(EntityId entityId);
    }
}