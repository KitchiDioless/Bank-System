using BankApp.Core.Entities;
using BankApp.Core.Entities.Mocks;
using BankApp.Core.Ports;

using Moq;
using Xunit;

namespace BankApp.Tests;

public class BankSystem
{
    [Fact]
    public void WithdrawFromAccount_ReturnsAccountBalance_Success()
    {
        var mockRepo = new Mock<IAccountRepository>();

        string accountNumber = "12345";
        var newAccount = new MockAccount("12345", "6789", 1000.0);
        var expectedAccount = new Account("12345", "6789", 90.0);

        newAccount.WithdrawMoneyFromAccount(accountNumber, 10.0);
        newAccount.WithdrawMoneyFromAccount(accountNumber, 10000.0);

        mockRepo.Setup(repo => repo.GetAccount(accountNumber))
                .Returns(expectedAccount);

        Account? actualAccount = mockRepo.Object.GetAccount(accountNumber);

        Assert.NotNull(actualAccount);
        Assert.Equal(expectedAccount.AccountNumber, actualAccount.AccountNumber);
        Assert.Equal(expectedAccount.PinCode, actualAccount.PinCode);
        Assert.Equal(expectedAccount.Money, actualAccount.Money);
    }

    [Fact]
    public void DepositToAccount_ReturnsAccountBalance_Success()
    {
        var mockRepo = new Mock<IAccountRepository>();

        string accountNumber = "12345";
        var newAccount = new MockAccount("12345", "6789", 1000.0);
        var expectedAccount = new Account("12345", "6789", 1010.0);

        newAccount.PutMoneyOnAccount(accountNumber, 10.0);

        mockRepo.Setup(repo => repo.GetAccount(accountNumber))
                .Returns(expectedAccount);

        Account? actualAccount = mockRepo.Object.GetAccount(accountNumber);

        Assert.NotNull(actualAccount);
        Assert.Equal(expectedAccount.AccountNumber, actualAccount.AccountNumber);
        Assert.Equal(expectedAccount.PinCode, actualAccount.PinCode);
        Assert.Equal(expectedAccount.Money, actualAccount.Money);
    }
}
