using BankApp.Application.Services;
using BankApp.Core.Interfaces;
using BankApp.Core.States.UserStates;
using System.Collections.Generic;

namespace BankApp.Core.Entities;

public class User : IUser
{
    public IUserState State { get; set; }

    public string? Id { get; }

    public string? Password { get; set; }

    public IList<IAccount> Accounts { get; set; }

    public User(IList<IAccount> accounts, string? id = null, string? password = null)
    {
        State = new UserStateDisconnected(this);
        Id = id;
        Password = password;
        Accounts = accounts;
    }

    public string Connect(RepositoryService repositoryService)
    {
        return State.Connect(repositoryService);
    }

    public string Disconnect()
    {
        return State.Disconnect();
    }

    public string ChangeToAdmin(string password)
    {
        return State.ChangeToAdmin(password);
    }

    public string ChangeToUser()
    {
        return State.ChangeToUser();
    }

    public string ChangeSystemPassword(string newPassword, RepositoryService repositoryService)
    {
        return State.ChangeSystemPassword(newPassword, repositoryService);
    }

    public string CreateAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        return State.CreateAccount(accountNumber, pinCode, repositoryService);
    }

    public string DeleteAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        return State.DeleteAccount(accountNumber, pinCode, repositoryService);
    }

    public string GetCurrentAmount(string accountNumber)
    {
        return State.GetCurrentAmount(accountNumber);
    }

    public string WithdrawMoneyFromAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        return State.WithdrawMoneyFromAccount(accountNumber, money, repositoryService);
    }

    public string PutMoneyOnAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        return State.PutMoneyOnAccount(accountNumber, money, repositoryService);
    }

    public string ChangePassword(string newPassword)
    {
        return State.ChangePassword(newPassword);
    }
}
