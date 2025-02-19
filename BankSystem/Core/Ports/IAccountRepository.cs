using BankApp.Core.Entities;

namespace BankApp.Core.Ports;

public interface IAccountRepository
{
    Account? GetAccount(string accountNumber);

    void SaveAccount(Account account);
}
