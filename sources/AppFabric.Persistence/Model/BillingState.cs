using AppFabric.Persistence.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Persistence.Model
{
    public class BillingState : PersistentState
    {
        public BillingState(byte[] rowVersion)
            : base(DateTime.Now, rowVersion)
        {

        }
    }
}
