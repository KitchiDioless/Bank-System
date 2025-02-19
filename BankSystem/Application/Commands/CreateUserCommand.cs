using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class CreateUserCommand : ICommand
{
    public CommandController Controller { get; }

    public RepositoryService RepositoryService { get; }

    private readonly string _login;

    private readonly string _password;

    public CreateUserCommand(CommandController controller, RepositoryService repositoryService, string login, string password)
    {
        Controller = controller;
        RepositoryService = repositoryService;
        _login = login;
        _password = password;
    }

    public string Execute()
    {
        return RepositoryService.CreateUser(_login, _password);
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return RepositoryService.DeleteUser(_login, _password);
    }
}
