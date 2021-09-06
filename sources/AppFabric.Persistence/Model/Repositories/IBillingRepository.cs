using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Persistence.Model.Repositories
{
    public interface IBillingRepository : IRepository<BillingState, Billing>
    {

    }
}
