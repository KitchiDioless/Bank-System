using BankApp.Application.Services;
using BankApp.Core.Entities;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class ConnectCommand : ICommand
{
    public CommandController Controller { get; }

    public RepositoryService RepositoryService { get; }

    private readonly string _id;

    private readonly string _password;

    public ConnectCommand(CommandController controller, RepositoryService repositoryService, string id, string password)
    {
        Controller = controller;
        RepositoryService = repositoryService;
        _id = id;
        _password = password;
    }

    public string Execute()
    {
        if (RepositoryService.AuthenticateAsUser(_id, _password))
        {
            Controller.User = new User(RepositoryService.GetUserAccounts(_id), _id, _password);
            return Controller.User.State.Connect(RepositoryService);
        }

        return "Authentication failed";
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.Disconnect();
    }
}
