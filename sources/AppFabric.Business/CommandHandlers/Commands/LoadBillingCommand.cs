using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class LoadBillingCommand : BaseCommand
    {
        public LoadBillingCommand(Billing billing)
        {
            Billing = billing;
        }

        public Billing Billing { get; }
    }
}