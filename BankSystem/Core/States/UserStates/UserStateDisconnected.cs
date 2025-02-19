using BankApp.Application.Services;
using BankApp.Core.Interfaces;

namespace BankApp.Core.States.UserStates;

public class UserStateDisconnected : IUserState
{
    public IUser User { get; }

    public UserStateDisconnected(IUser user)
    {
        User = user;
    }

    public string Connect(RepositoryService repositoryService)
    {
        if (User.Id == null)
        {
            return "You are already connected";
        }

        User.State = new UserStateConnected(User);

        return "You are connected";
    }

    public string Disconnect()
    {
        return "You are already disconnected";
    }

    public string ChangeToAdmin(string password)
    {
        return "You can't change to admin. You are disconnected.";
    }

    public string ChangeToUser()
    {
        return "You can't change to user. You are disconnected.";
    }

    public string ChangeSystemPassword(string newPassword, RepositoryService repositoryService)
    {
        return "You can't change system password. You are disconnected.";
    }

    public string CreateAccount(string accountNumber, string pinCode)
    {
        return "You can't create account. You are disconnected.";
    }

    public string CreateAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        return "You can't create account. You are disconnected.";
    }

    public string DeleteAccount(string accountNumber, string pinCode, RepositoryService repositoryService)
    {
        return "You can't delete account. You are disconnected.";
    }

    public string GetCurrentAmount(string accountNumber)
    {
        return "You can't create account. You are disconnected.";
    }

    public string WithdrawMoneyFromAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        return "You can't withdraw money. You are disconnected.";
    }

    public string PutMoneyOnAccount(string accountNumber, double money, RepositoryService repositoryService)
    {
        return "You can't put money. You are disconnected.";
    }

    public string ChangePassword(string newPassword)
    {
        return "You can't change password. You are disconnected.";
    }
}
