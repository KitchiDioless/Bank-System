using BankApp.Application.Services;
using BankApp.Core.Interfaces;
using System.Linq;

namespace BankApp.Application.Commands;

public class ChangePincodeCommand : ICommand
{
    public CommandController Controller { get; }

    private readonly string _accountNumber;

    private readonly string _newPassword;

    private string? _prevPassword;

    public ChangePincodeCommand(CommandController controller, string accountNumber, string newPassword)
    {
        Controller = controller;
        _newPassword = newPassword;
        _prevPassword = null;
        _accountNumber = accountNumber;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        _prevPassword = Controller.User.Password;
        return Controller.User.Accounts.First(account => account.AccountNumber == _accountNumber).ChangePinCode(_newPassword);
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        if (_prevPassword == null)
        {
            return "Password has not been changed.";
        }

        string? prev = _prevPassword;
        _prevPassword = Controller.User.Password;
        return Controller.User.Accounts.First(account => account.AccountNumber == _accountNumber).ChangePinCode(prev);
    }
}
