using BankApp.Application.Services;

namespace BankApp.Core.Interfaces;

public interface IAccount
{
    IAccountState State { get; set; }

    string AccountNumber { get; }

    string PinCode { get; set; }

    double Money { get; set; }

    string GetCurrentAmount();

    string WithdrawMoneyFromAccount(string userName, double money, RepositoryService repositoryService);

    string PutMoneyOnAccount(string userName, double money, RepositoryService repositoryService);

    string ChangePinCode(string newPinCode);
}
