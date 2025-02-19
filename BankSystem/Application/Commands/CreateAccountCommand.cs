using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class CreateAccountCommand : ICommand
{
    public CommandController Controller { get; }

    public RepositoryService RepositoryService { get; }

    private readonly string _id;

    private readonly string _pin;

    public CreateAccountCommand(CommandController controller, RepositoryService repositoryService, string id, string pin)
    {
        Controller = controller;
        RepositoryService = repositoryService;
        _id = id;
        _pin = pin;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.CreateAccount(_id, _pin, RepositoryService);
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.DeleteAccount(_id, _pin, RepositoryService);
    }
}
