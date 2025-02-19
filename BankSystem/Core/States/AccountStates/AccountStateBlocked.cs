using BankApp.Application.Services;
using BankApp.Core.Entities;
using BankApp.Core.Interfaces;

namespace BankApp.Core.States.AccountStates;

public class AccountStateBlocked : IAccountState
{
    public Account Account { get; }

    public AccountStateBlocked(Account account)
    {
        Account = account;
    }

    public string GetCurrentAmount()
    {
        return "Account is blocked";
    }

    public string WithdrawMoneyFromAccount(string userName, double money, RepositoryService repositoryService)
    {
        return "Account is blocked";
    }

    public string PutMoneyOnAccount(string userName, double money, RepositoryService repositoryService)
    {
        return "Account is blocked";
    }

    public string ChangePinCode(string newPinCode)
    {
        return "Account is blocked";
    }
}
