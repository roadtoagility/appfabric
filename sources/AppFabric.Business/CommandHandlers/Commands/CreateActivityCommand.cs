using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CreateActivityCommand
    {
        public Guid Id { get; set; } 
        public Guid ProjectId { get; set; }
        public int Hours { get; set; }
    }
}
