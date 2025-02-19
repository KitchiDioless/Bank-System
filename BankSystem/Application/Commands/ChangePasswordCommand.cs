using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class ChangePasswordCommand : ICommand
{
    public CommandController Controller { get; }

    private readonly string _newPassword;

    private string? _prevPassword;

    public ChangePasswordCommand(CommandController controller, string newPassword)
    {
        Controller = controller;
        _newPassword = newPassword;
        _prevPassword = null;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        _prevPassword = Controller.User.Password;
        return Controller.User.ChangePassword(_newPassword);
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        string? prev = _prevPassword;

        if (prev == null)
        {
            return "Password has not been changed.";
        }

        _prevPassword = Controller.User.Password;
        return Controller.User.ChangePassword(prev);
    }
}
