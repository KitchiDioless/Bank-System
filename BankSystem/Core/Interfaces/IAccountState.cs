using BankApp.Application.Services;

namespace BankApp.Core.Interfaces;

public interface IAccountState
{
    string GetCurrentAmount();

    string WithdrawMoneyFromAccount(string userName, double money, RepositoryService repositoryService);

    string PutMoneyOnAccount(string userName, double money, RepositoryService repositoryService);

    string ChangePinCode(string newPinCode);
}
