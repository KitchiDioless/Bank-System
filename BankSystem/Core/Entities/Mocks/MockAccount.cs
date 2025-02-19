namespace BankApp.Core.Entities.Mocks;

public class MockAccount
{
    public string AccountNumber { get; set; }

    public string PinCode { get; set; }

    public double Money { get; set; }

    public MockAccount(string accountNumber, string pinCode, double money = 0)
    {
        AccountNumber = accountNumber;
        PinCode = pinCode;
        Money = money;
    }

    public string GetCurrentAmount()
    {
        return Money.ToString();
    }

    public string ChangePinCode(string newPinCode)
    {
        return "Changed";
    }

    public string PutMoneyOnAccount(string userName, double money)
    {
        Money += money;
        return "Success";
    }

    public string WithdrawMoneyFromAccount(string userName, double money)
    {
        if (Money <= money)
        {
            return "Error";
        }

        Money -= money;

        return "Success";
    }
}
