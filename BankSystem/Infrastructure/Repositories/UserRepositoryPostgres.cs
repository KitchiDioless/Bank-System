using FluentMigrator.Runner;
using BankApp.Core.Entities;
using BankApp.Core.Interfaces;
using BankApp.Core.Ports;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Collections.Generic;

namespace BankApp.Infrastructure.Repositories;

public class UserRepositoryPostgres : IUserRepository
{
    private readonly string _connectionString;

    public UserRepositoryPostgres(string connectionString)
    {
        _connectionString = connectionString;
        RunMigrations();
    }

    public User? GetUser(string userName)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT \"Id\", \"Password\" FROM \"Users\" WHERE \"Id\" = @userName";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("userName", userName);

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var user = new User(
                new List<IAccount>(),
                reader.GetString(0),
                reader.GetString(1));

            if (user.Id != null)
            {
                user.Accounts = GetUserAccounts(user.Id);
            }

            return user;
        }

        return null;
    }

    public IList<IAccount> GetUserAccounts(string user)
    {
        var accounts = new List<IAccount>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT \"AccountNumber\", \"PinCode\", \"Balance\" FROM \"Accounts\" WHERE \"UserId\" = @userId";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("userId", user);

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            accounts.Add(new Account(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetDouble(2)));
        }

        return accounts;
    }

    public void DeleteUser(string userName)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using NpgsqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string query = "DELETE FROM \"Users\" WHERE \"Id\" = @userName";
            using var cmd = new NpgsqlCommand(query, connection, transaction);
            cmd.Parameters.AddWithValue("userName", userName);
            cmd.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void AddUser(string name, string password)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO \"Users\" (\"Id\", \"Password\") VALUES (@id, @password) ON CONFLICT (\"Id\") DO NOTHING";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("id", name);
        cmd.Parameters.AddWithValue("password", password);
        cmd.ExecuteNonQuery();
    }

    public void SaveUser(User user)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using NpgsqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string query = "INSERT INTO \"Users\" (\"Id\", \"Password\") VALUES (@id, @password) ON CONFLICT (\"Id\") DO NOTHING";
            using var cmd = new NpgsqlCommand(query, connection, transaction);
            if (user.Id != null)
            {
                cmd.Parameters.AddWithValue("id", user.Id);
            }

            if (user.Password != null)
            {
                cmd.Parameters.AddWithValue("password", user.Password);
            }

            cmd.ExecuteNonQuery();

            foreach (IAccount account in user.Accounts)
            {
                string accountQuery = "INSERT INTO \"Accounts\" (\"UserId\", \"PinCode\", \"Balance\") VALUES (@userId, @pinCode, @balance)";
                using var accountCmd = new NpgsqlCommand(accountQuery, connection, transaction);

                if (user.Id != null)
                {
                    accountCmd.Parameters.AddWithValue("userId", user.Id);
                }

                accountCmd.Parameters.AddWithValue("pinCode", account.PinCode);
                accountCmd.Parameters.AddWithValue("balance", account.Money);
                accountCmd.ExecuteNonQuery();
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void AddAccount(string userName, string accountName, string pincode)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO \"Accounts\" (\"UserId\", \"AccountNumber\", \"PinCode\", \"Balance\") VALUES (@userId, @accountNumber, @pinCode, 0)";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("userId", userName);
        cmd.Parameters.AddWithValue("accountNumber", accountName);
        cmd.Parameters.AddWithValue("pinCode", pincode);
        cmd.ExecuteNonQuery();
    }

    public void DeleteAccount(string userName, string accountName, string pincode)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using NpgsqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string query = "DELETE FROM \"Accounts\" WHERE \"UserId\" = @userId AND \"AccountNumber\" = @accountNumber AND \"PinCode\" = @pinCode";
            using var cmd = new NpgsqlCommand(query, connection, transaction);
            cmd.Parameters.AddWithValue("userId", userName);
            cmd.Parameters.AddWithValue("accountNumber", accountName);
            cmd.Parameters.AddWithValue("pinCode", pincode);
            cmd.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void UpdateAccount(string userName, string accountName, double money)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using NpgsqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string query = "UPDATE \"Accounts\" SET \"Balance\" = @balance WHERE \"UserId\" = @userId AND \"AccountNumber\" = @accountNumber";
            using var cmd = new NpgsqlCommand(query, connection, transaction);
            cmd.Parameters.AddWithValue("balance", money);
            cmd.Parameters.AddWithValue("userId", userName);
            cmd.Parameters.AddWithValue("accountNumber", accountName);
            cmd.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private void RunMigrations()
    {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(_connectionString)
                .ScanIn(typeof(Database.Migrations.InitialMigration).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();

        using IServiceScope scope = serviceProvider.CreateScope();
        IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        runner.MigrateUp();
    }
}
