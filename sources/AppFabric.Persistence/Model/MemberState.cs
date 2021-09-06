using AppFabric.Persistence.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
