using BankApp.Application.Services;

namespace BankApp.Core.Interfaces;

public interface ICommand
{
    CommandController Controller { get; }

    string Execute();

    string Undo();
}
