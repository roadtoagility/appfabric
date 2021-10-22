using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class LoadProjectCommand : BaseCommand
    {
        public Project Project { get; }

        public LoadProjectCommand(Project project)
        {
            Project = project;
        }
    }
}
