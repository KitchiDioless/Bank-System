using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class BalanceCommand : ICommand
{
    public CommandController Controller { get; }

    private readonly string _accountNumber;

    public BalanceCommand(CommandController controller, string accountNumber)
    {
        Controller = controller;
        _accountNumber = accountNumber;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.GetCurrentAmount(_accountNumber);
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.GetCurrentAmount(_accountNumber);
    }
}
