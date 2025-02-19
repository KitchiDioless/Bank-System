using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class DisconnectCommand : ICommand
{
    public CommandController Controller { get; }

    public RepositoryService RepositoryService { get; }

    public DisconnectCommand(CommandController controller, RepositoryService repositoryService)
    {
        Controller = controller;
        RepositoryService = repositoryService;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.Disconnect();
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.State.Connect(RepositoryService);
    }
}
