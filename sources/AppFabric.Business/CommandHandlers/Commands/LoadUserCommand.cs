using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Command;

namespace AppFabric.Business.CommandHandlers.Commands
{
    public class LoadUserCommand : BaseCommand
    {
        public LoadUserCommand(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}