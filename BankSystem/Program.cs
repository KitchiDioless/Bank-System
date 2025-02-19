using BankApp.Application.Parsers;
using BankApp.Application.Services;
using BankApp.Core.Ports;
using BankApp.Infrastructure.Repositories;
using BankApp.Presentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab5;

public class Program
{
    public static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        ServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton(configuration)

            .AddScoped<IUserRepository>(sp =>
            {
                string? connectionString = configuration.GetSection("AppSettings")["DatabaseConnection"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Database connection string not found in appsettings.json");
                }

                return new UserRepositoryPostgres(connectionString);
            })

            .AddSingleton<ConfigurationService>()
            .AddSingleton<IParser, ConsoleParser>()
            .AddSingleton<ConsoleAdapter>()
            .BuildServiceProvider();

        ConsoleAdapter consoleAdapter = serviceProvider.GetRequiredService<ConsoleAdapter>();

        while (true)
        {
            consoleAdapter.SetConsoleOutput("Enter a command: ");
            consoleAdapter.SetConsoleOutput(consoleAdapter.ReadConsoleInput());
        }
    }
}