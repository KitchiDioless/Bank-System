using BankApp.Core.Interfaces;
using BankApp.Core.Ports;
using System;
using System.Collections.Generic;

namespace BankApp.Application.Services;

public class RepositoryService
{
    private readonly ConfigurationService _configurationService;

    private readonly IUserRepository _userRepository;

    private int _count;

    public RepositoryService(ConfigurationService configurationService, IUserRepository userRepository)
    {
        _configurationService = configurationService;
        _userRepository = userRepository;
        _count = 0;
    }

    public bool AuthenticateAsAdmin(string password)
    {
        bool result = password == _configurationService.GetSystemPassword();

        if (!result)
        {
            _count++;
        }

        if (_count >= 3)
        {
            throw new Exception("Too many attempts to authenticate as admin.");
        }

        return result;
    }

    public bool AuthenticateAsUser(string name, string pin)
    {
        Core.Entities.User? user = _userRepository.GetUser(name);
        if (user == null)
        {
            return false;
        }

        return user.Password == pin;
    }

    public string? GetSystemPassword()
    {
        return _configurationService.GetSystemPassword();
    }

    public void SetSystemPassword(string password)
    {
        _configurationService.SetSystemPassword(password);
    }

    public string CreateUser(string name, string password)
    {
        if (_userRepository.GetUser(name) != null)
        {
            return "User already exists";
        }

        _userRepository.AddUser(name, password);

        return "User created";
    }

    public string DeleteUser(string name, string password)
    {
        Core.Entities.User? user = _userRepository.GetUser(name);
        if (user == null)
        {
            return "User does not exist";
        }

        if (user.Password != password)
        {
            return "Wrong password";
        }

        _userRepository.DeleteUser(name);

        return "User deleted";
    }

    public void AddAccount(string name, string accountNumber, string pinCode)
    {
        _userRepository.AddAccount(name, accountNumber, pinCode);
    }

    public void DeleteAccount(string name, string accountNumber, string pinCode)
    {
        _userRepository.DeleteAccount(name, accountNumber, pinCode);
    }

    public IList<IAccount> GetUserAccounts(string name)
    {
        if (_userRepository == null)
        {
            return new List<IAccount>();
        }

        return _userRepository.GetUserAccounts(name);
    }

    public void UpdateAccount(string userName, string accountName, double money)
    {
        _userRepository.UpdateAccount(userName, accountName, money);
    }
}
