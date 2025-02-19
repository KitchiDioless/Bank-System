using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Core.States.AccountStates;

public class AccountStateDefault : IAccountState
{
    public IAccount Account { get; }

    public AccountStateDefault(IAccount account)
    {
        Account = account;
    }

    public string GetCurrentAmount()
    {
        return Account.Money.ToString();
    }

    public string WithdrawMoneyFromAccount(string userName, double money, RepositoryService repositoryService)
    {
        if (money <= 0)
        {
            return "Incorrect amount. Should be positive.";
        }

        if (Account.Money < money)
        {
            return "Not enough money.";
        }

        Account.Money -= money;

        repositoryService.UpdateAccount(userName, Account.AccountNumber, Account.Money);

        return "Money withdrawn";
    }

    public string PutMoneyOnAccount(string userName, double money, RepositoryService repositoryService)
    {
        if (money <= 0)
        {
            return "Incorrect amount. Should be positive.";
        }

        Account.Money += money;

        repositoryService.UpdateAccount(userName, Account.AccountNumber, Account.Money);

        return "Money put on account";
    }

    public string ChangePinCode(string newPinCode)
    {
        Account.PinCode = newPinCode;
        return "Pin code changed";
    }
}
