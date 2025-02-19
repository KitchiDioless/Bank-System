using BankApp.Application.Commands;
using BankApp.Application.Services;
using BankApp.Core.Ports;
using System;

namespace BankApp.Application.Parsers;

public class ConsoleParser : IParser
{
    private readonly CommandController _commandController;

    private readonly RepositoryService _repositoryService;

    public ConsoleParser(ConfigurationService configurationService, IUserRepository userRepository)
    {
        _commandController = new CommandController();
        _repositoryService = new RepositoryService(configurationService, userRepository);
    }

    public string ParseCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "Error: Command is empty.";

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return "Error: Command is empty.";

        string command = parts[0].ToLowerInvariant();

        return command switch
        {
            "connect" => ParseConnectCommand(parts),
            "disconnect" => ParseDisconnectCommand(parts),
            "create" => ParseCreateCommand(parts),
            "withdraw" => ParseWithdrawCommand(parts),
            "deposit" => ParseDepositCommand(parts),
            "balance" => ParseBalanceCommand(parts),
            "history" => ParseHistoryCommand(parts),
            "undo" => _commandController.UndoLastCommand(),
            "change" => ParseChangeCommand(parts),
            "changetoadmin" => ParseChangeToAdminCommand(parts),
            "changetouser" => ParseChangeToUserCommand(parts),
            "help" => ShowHelp(),
            _ => "Error: Unknown command. Type \"help\" to display all commands.",
        };
    }

    private string ParseConnectCommand(string[] parts)
    {
        if (parts.Length < 3)
            return "Error: Invalid arguments.";

        var connectCommand = new ConnectCommand(_commandController, _repositoryService, parts[1], parts[2]);
        return _commandController.ExecuteCommand(connectCommand);
    }

    private string ParseDisconnectCommand(string[] parts)
    {
        if (parts.Length != 1)
            return "Error: Invalid arguments.";

        var disconnectCommand = new DisconnectCommand(_commandController, _repositoryService);
        return _commandController.ExecuteCommand(disconnectCommand);
    }

    private string ParseCreateCommand(string[] parts)
    {
        if (parts.Length == 4 && parts[1] == "user")
        {
            var createCommand = new CreateUserCommand(_commandController, _repositoryService, parts[2], parts[3]);
            return _commandController.ExecuteCommand(createCommand);
        }

        if (parts.Length == 4 && parts[1] == "account")
        {
            var createAccountCommand = new CreateAccountCommand(_commandController, _repositoryService, parts[2], parts[3]);
            return _commandController.ExecuteCommand(createAccountCommand);
        }

        return "Error: Invalid arguments.";
    }

    private string ParseWithdrawCommand(string[] parts)
    {
        if (parts.Length < 3 || !decimal.TryParse(parts[2], out decimal withdrawAmount))
            return "Error: Invalid amount for withdrawal.";

        var withdrawCommand = new WithdrawCommand(_commandController, parts[1], (double)withdrawAmount, _repositoryService);
        return _commandController.ExecuteCommand(withdrawCommand);
    }

    private string ParseDepositCommand(string[] parts)
    {
        if (parts.Length < 3 || !decimal.TryParse(parts[2], out decimal depositAmount))
            return "Error: Invalid amount for deposit.";

        var depositCommand = new DepositCommand(_commandController, parts[1], (double)depositAmount, _repositoryService);
        return _commandController.ExecuteCommand(depositCommand);
    }

    private string ParseBalanceCommand(string[] parts)
    {
        var balanceCommand = new BalanceCommand(_commandController, parts[1]);
        return _commandController.ExecuteCommand(balanceCommand);
    }

    private string ParseHistoryCommand(string[] parts)
    {
        var historyCommand = new HistoryCommand(_commandController);
        return _commandController.ExecuteCommand(historyCommand);
    }

    private string ParseChangeCommand(string[] parts)
    {
        if (parts.Length == 3 && parts[1] == "password")
        {
            var changePasswordCommand = new ChangePasswordCommand(_commandController, parts[2]);
            return _commandController.ExecuteCommand(changePasswordCommand);
        }

        if (parts.Length == 4 && parts[1] == "pincode")
        {
            var changePincodeCommand = new ChangePincodeCommand(_commandController, parts[2], parts[3]);
            return _commandController.ExecuteCommand(changePincodeCommand);
        }

        if (parts.Length == 3 && parts[1] == "system_password")
        {
            var changeSystemPasswordCommand = new ChangeSystemPasswordCommand(_commandController, _repositoryService, parts[2]);
            return _commandController.ExecuteCommand(changeSystemPasswordCommand);
        }

        return "Error: Cant be changed.";
    }

    private string ParseChangeToAdminCommand(string[] parts)
    {
        if (parts.Length == 2)
        {
            var changeToAdmin = new ChangeToAdminCommand(_commandController, _repositoryService, parts[1]);
            return _commandController.ExecuteCommand(changeToAdmin);
        }

        return "Error: Cant be changed.";
    }

    private string ParseChangeToUserCommand(string[] parts)
    {
        if (parts.Length == 1)
        {
            var changeToUser = new ChangeToUserCommand(_commandController);
            return _commandController.ExecuteCommand(changeToUser);
        }

        return "Error: Cant be changed.";
    }

    private string ShowHelp()
    {
        return "|==============================================================================|\n" +
               "\tAvailable commands:\n" +
               "\tconnect [id] [password] - connect to the system\n" +
               "\tdisconnect - disconnect from the system\n" +
               "\tcreate user [id] [password] - create a new user\n" +
               "\tcreate account [id] [pincode] - create a new account\n" +
               "\twithdraw [account_id] [amount] - withdraw money from the account\n" +
               "\tdeposit [account_id] [amount] - deposit money to the account\n" +
               "\tbalance [account_id] - get the balance of the account\n" +
               "\thistory - get the history of the last commands\n" +
               "\tundo - undo the last command\n" +
               "\tchange password [new_password] - change the password of the user\n" +
               "\tchange pincode [account_id] [new_pincode] - change the account pincode\n" +
               "\tchange system_password [new_password] - change the system password\n" +
               "\tchangetoadmin [password] - change the user to admin\n" +
               "\tchangetouser - change the user to user\n" +
               "\thelp - show this help message\n" +
               "|==============================================================================|\n";
    }
}
