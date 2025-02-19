using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Core.States.UserStates;

public class UserStateBlocked : IUserState
{
    public IUser User { get; }

    public UserStateBlocked(IUser user)
    {
        User = user;
    }

    public string Connect(RepositoryService repositoryService)
    {
        return "You can't connect. You are blocked.";
    }

    public string Disconnect()
    {
        return "You can't disconnect. You are blocked.";
    }

    public string ChangeToAdmin(string password)
    {
        return "You can't change to admin. You are blocked.";
    }

    public string ChangeToUser()
    {
        return "You can't change to user. You are blocked.";
    }

    public string ChangeSystemPassword(string newPassword, RepositoryService repositoryService)
    {
        return "You can't change system password. You are blocked.";
    }

    public string CreateAccount(string accountNumber, string pinCode)
    {
        return "You can't create account. You are blocked.";
    }

    public string CreateAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        return "You can't create account. You are blocked.";
    }

    public string DeleteAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        return "You can't delete account. You are blocked.";
    }

    public string GetCurrentAmount(string accountNumber)
    {
        return "You can't create account. You are blocked.";
    }

    public string WithdrawMoneyFromAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        return "You can't withdraw money. You are blocked.";
    }

    public string PutMoneyOnAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        return "You can't put money. You are blocked.";
    }

    public string ChangePassword(string newPassword)
    {
        return "You can't change password. You are blocked.";
    }
}
