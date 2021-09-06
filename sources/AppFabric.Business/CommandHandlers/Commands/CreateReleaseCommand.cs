using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CreateReleaseCommand
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
    }
}
