using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Application.Commands;

public class WithdrawCommand : ICommand
{
    public CommandController Controller { get; }

    public RepositoryService RepositoryService { get; }

    private readonly double _amount;

    private readonly string _accountNumber;

    public WithdrawCommand(CommandController controller, string accountNumber, double amount, RepositoryService repositoryService)
    {
        Controller = controller;
        RepositoryService = repositoryService;
        _amount = amount;
        _accountNumber = accountNumber;
    }

    public string Execute()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.WithdrawMoneyFromAccount(_accountNumber, _amount, RepositoryService);
    }

    public string Undo()
    {
        if (Controller.User == null)
        {
            return "User is not set";
        }

        return Controller.User.PutMoneyOnAccount(_accountNumber, _amount, RepositoryService);
    }
}
