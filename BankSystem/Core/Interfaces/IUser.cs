using BankApp.Application.Services;
using System.Collections.Generic;

namespace BankApp.Core.Interfaces;

public interface IUser
{
    IUserState State { get; set; }

    IList<IAccount> Accounts { get; }

    string? Id { get; }

    string? Password { get; set; }

    string Connect(RepositoryService repositoryService);

    string Disconnect();

    string ChangeToAdmin(string password);

    string ChangeToUser();

    string ChangeSystemPassword(string newPassword, RepositoryService repositoryService);

    string CreateAccount(string accountNumber, string pinCode, RepositoryService repositoryService);

    string DeleteAccount(string accountNumber, string pinCode, RepositoryService repositoryService);

    string GetCurrentAmount(string accountNumber);

    string WithdrawMoneyFromAccount(string accountNumber, double money, RepositoryService repositoryService);

    string PutMoneyOnAccount(string accountNumber, double money, RepositoryService repositoryService);

    string ChangePassword(string newPassword);
}
