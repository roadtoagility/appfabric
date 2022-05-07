using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class LoadReleaseCommand : BaseCommand
    {
        public LoadReleaseCommand(Release release)
        {
            Release = release;
        }

        public Release Release { get; }
    }
}