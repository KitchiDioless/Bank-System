using Microsoft.Extensions.Configuration;

namespace BankApp.Application.Services;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GetSystemPassword()
    {
        return _configuration["AppSettings:SystemPassword"];
    }

    public void SetSystemPassword(string password)
    {
        _configuration["AppSettings:SystemPassword"] = password;
    }

    public string? GetDatabaseConnectionString()
    {
        return _configuration["AppSettings:DatabaseConnection"];
    }
}