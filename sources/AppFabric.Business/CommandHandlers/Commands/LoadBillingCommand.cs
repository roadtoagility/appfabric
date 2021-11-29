using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class LoadBillingCommand : BaseCommand
    {
        public Billing Billing { get; }

        public LoadBillingCommand(Billing billing)
        {
            Billing = billing;
        }
    }
}
