using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class LoadProjectCommand : BaseCommand
    {
        public LoadProjectCommand(Project project)
        {
            Project = project;
        }

        public Project Project { get; }
    }
}