namespace BankApp.Core.Ports;

public interface IParser
{
    string ParseCommand(string input);
}
