using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class LoadActivityCommand : BaseCommand
    {
        public LoadActivityCommand(Activity activity)
        {
            Activity = activity;
        }

        public Activity Activity { get; }
    }
}