using System;
using DFlow.Persistence.Model;

namespace AppFabric.Persistence.Model
{
    public class MemberState : PersistentState
    {
        public MemberState(byte[] rowVersion)
            : base(DateTime.Now, rowVersion)
        {
        }
    }
}