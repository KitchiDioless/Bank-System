using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class ChangeToUserCommand : ICommand
{
    public CommandController Controller { get; }

    public ChangeToUserCommand(CommandController controller)
    {
        Controller = controller;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.ChangeToUser();
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
