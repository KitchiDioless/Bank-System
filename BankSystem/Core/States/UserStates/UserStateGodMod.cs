using BankApp.Application.Services;
using BankApp.Core.Entities;
using BankApp.Core.Interfaces;
using System.Linq;

namespace BankApp.Core.States.UserStates;

public class UserStateGodMod : IUserState
{
    public IUser User { get; }

    public UserStateGodMod(IUser user)
    {
        User = user;
    }

    public string Connect(RepositoryService repositoryService)
    {
        return "You are already connected";
    }

    public string Disconnect()
    {
        User.State = new UserStateDisconnected(User);
        return "You are disconnected";
    }

    public string ChangeToAdmin(string password)
    {
        return "You are already admin";
    }

    public string ChangeToUser()
    {
        User.State = new UserStateConnected(User);
        return "Changed to user";
    }

    public string ChangeSystemPassword(string newPassword, RepositoryService repositoryService)
    {
        repositoryService.SetSystemPassword(newPassword);

        return "System password changed";
    }

    public string CreateAccount(string accountNumber, string pinCode)
    {
        if (User.Id == null)
        {
            return "You are not connected";
        }

        User.Accounts.Add(new Account(accountNumber, pinCode));
        return "Account created";
    }

    public string CreateAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        if (User.Id == null)
        {
            return "You are not connected";
        }

        repositoryService.AddAccount(User.Id, accountNumber, pinCode);
        User.Accounts.Add(new Account(accountNumber, pinCode));

        return "Account created";
    }

    public string DeleteAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        if (User.Id == null)
        {
            return "You are not connected";
        }

        repositoryService.DeleteAccount(User.Id, accountNumber, pinCode);
        User.Accounts.Remove(User.Accounts.First(account => account.AccountNumber == accountNumber));

        return "Account deleted";
    }

    public string GetCurrentAmount(string accountNumber)
    {
        return User.Accounts.First(account => account.AccountNumber == accountNumber).GetCurrentAmount();
    }

    public string WithdrawMoneyFromAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        if (User.Accounts.Count == 0 || User.Id == null)
        {
            return "You don't have any accounts";
        }

        User.Accounts.First(account => account.AccountNumber == accountNumber).WithdrawMoneyFromAccount(User.Id, money, repositoryService);
        return $"Withdrawn {money} from account {accountNumber}";
    }

    public string PutMoneyOnAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        if (User.Accounts.Count == 0 || User.Id == null)
        {
            return "You don't have any accounts";
        }

        User.Accounts.First(account => account.AccountNumber == accountNumber).PutMoneyOnAccount(User.Id, money, repositoryService);
        return $"Put {money} on account {User.Accounts.First().AccountNumber}";
    }

    public string ChangePassword(string newPassword)
    {
        User.Password = newPassword;
        return "Password changed";
    }
}
