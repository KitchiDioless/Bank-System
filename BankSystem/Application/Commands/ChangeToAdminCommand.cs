using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class ChangeToAdminCommand : ICommand
{
    public CommandController Controller { get; }

    public RepositoryService RepositoryService { get; }

    private readonly string _password;

    public ChangeToAdminCommand(CommandController controller, RepositoryService repositoryService, string password)
    {
        Controller = controller;
        _password = password;
        RepositoryService = repositoryService;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        if (RepositoryService.AuthenticateAsAdmin(_password))
        {
            return Controller.User.ChangeToAdmin(_password);
        }

        return "Wrong password";
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.ChangeToUser();
    }
}
