using System;
using DFlow.Persistence.Model;

namespace AppFabric.Persistence.Model
{
    public class ReleaseState : PersistentState
    {
        public ReleaseState(byte[] rowVersion)
            : base(DateTime.Now, rowVersion)
        {
        }
    }
}