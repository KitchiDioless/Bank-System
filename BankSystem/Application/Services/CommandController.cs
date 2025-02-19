using BankApp.Core.Interfaces;
using System.Collections.Generic;

namespace BankApp.Application.Services;

public class CommandController
{
    public Stack<ICommand> Commands { get; private set; }

    public IUser? User { get; set; }

    public CommandController()
    {
        Commands = new Stack<ICommand>();
        User = null;
    }

    public string ExecuteCommand(ICommand command)
    {
        Commands.Push(command);
        return command.Execute();
    }

    public string UndoLastCommand()
    {
        if (Commands == null)
        {
            return "No commands to undo";
        }

        if (Commands.Count == 0)
        {
            return "No commands to undo";
        }

        ICommand command = Commands.Pop();

        return command.Undo();
    }

    public string GetOperationHistory()
    {
        string output = string.Empty;

        foreach (ICommand command in Commands)
        {
            output += command.ToString();
            output += '\n';
        }

        return output;
    }
}
