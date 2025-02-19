using BankApp.Application.Services;
using BankApp.Core.Interfaces;
using BankApp.Core.States.AccountStates;

namespace BankApp.Core.Entities;

public class Account : IAccount
{
    public IAccountState State { get; set; }

    public string AccountNumber { get; set; }

    public string PinCode { get; set; }

    public double Money { get; set; }

    public Account(string accountNumber, string pinCode, double money = 0)
    {
        State = new AccountStateDefault(this);
        AccountNumber = accountNumber;
        PinCode = pinCode;
        Money = money;
    }

    public string GetCurrentAmount()
    {
        return State.GetCurrentAmount();
    }

    public string ChangePinCode(string newPinCode)
    {
        return State.ChangePinCode(newPinCode);
    }

    public string PutMoneyOnAccount(string userName, double money, RepositoryService repositoryService)
    {
        return State.PutMoneyOnAccount(userName, money, repositoryService);
    }

    public string WithdrawMoneyFromAccount(string userName, double money, RepositoryService repositoryService)
    {
        return State.WithdrawMoneyFromAccount(userName, money, repositoryService);
    }
}
