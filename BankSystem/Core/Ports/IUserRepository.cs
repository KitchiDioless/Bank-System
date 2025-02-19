using BankApp.Core.Entities;
using BankApp.Core.Interfaces;
using System.Collections.Generic;

namespace BankApp.Core.Ports;

public interface IUserRepository
{
    User? GetUser(string userName);

    void SaveUser(User user);

    void AddUser(string name, string password);

    void DeleteUser(string userName);

    void AddAccount(string userName, string accountName, string pincode);

    void DeleteAccount(string userName, string accountName, string pincode);

    IList<IAccount> GetUserAccounts(string user);

    void UpdateAccount(string userName, string accountName, double money);
}
