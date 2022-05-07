using System;
using DFlow.Persistence.Model;

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