using BankApp.Core.Ports;
using System;

namespace BankApp.Presentation;

public class ConsoleAdapter
{
    private readonly IParser _parser;

    public ConsoleAdapter(IParser parser)
    {
        _parser = parser;
    }

    public string ReadConsoleInput()
    {
        Console.Write("> ");

        string? input = Console.ReadLine();
        if (input != null)
            return _parser.ParseCommand(input);

        return string.Empty;
    }

    public void SetConsoleOutput(string output)
    {
        Console.WriteLine(output);
    }
}
