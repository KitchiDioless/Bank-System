using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class HistoryCommand : ICommand
{
    public CommandController Controller { get; }

    public HistoryCommand(CommandController controller)
    {
        Controller = controller;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.GetOperationHistory();
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.GetOperationHistory();
    }
}
