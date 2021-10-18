using DFlow.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class CreateReleaseCommand : BaseCommand
    {
        public Guid ClientId { get; }

        public CreateReleaseCommand(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
