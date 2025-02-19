using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class ChangeSystemPasswordCommand : ICommand
{
    public CommandController Controller { get; }

    public RepositoryService RepositoryService { get; }

    private readonly string _newPassword;

    private string? _prevPassword;

    public ChangeSystemPasswordCommand(CommandController controller, RepositoryService repositoryService, string newPassword)
    {
        Controller = controller;
        RepositoryService = repositoryService;
        _newPassword = newPassword;
        _prevPassword = null;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        _prevPassword = RepositoryService.GetSystemPassword();

        return Controller.User.ChangeSystemPassword(_newPassword, RepositoryService);
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        string? prev = _prevPassword;
        _prevPassword = RepositoryService.GetSystemPassword();

        if (prev == null)
        {
            return "Password has not been changed.";
        }

        return Controller.User.ChangeSystemPassword(prev, RepositoryService);
    }
}
