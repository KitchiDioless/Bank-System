using BankApp.Core.Entities;
using BankApp.Core.Ports;
using Npgsql;

namespace BankApp.Infrastructure.Repositories;

public class AccountRepositoryPostgres : IAccountRepository
{
    private readonly string _connectionString;

    public AccountRepositoryPostgres(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Account? GetAccount(string accountNumber)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT AccountNumber, Balance FROM Accounts WHERE AccountNumber = @accountNumber";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.Add(new NpgsqlParameter("accountNumber", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = accountNumber });

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var account = new Account(reader.GetString(0), reader.GetString(1), reader.GetDouble(2));

            return account;
        }

        return null;
    }

    public void SaveAccount(Account account)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO Accounts (AccountNumber, Balance) VALUES (@accountNumber, @balance)";

        using var cmd = new NpgsqlCommand(query, connection);

        cmd.Parameters.AddWithValue("accountNumber", account.AccountNumber);
        cmd.Parameters.AddWithValue("pincode", account.PinCode);
        cmd.Parameters.AddWithValue("balance", account.Money);

        cmd.ExecuteNonQuery();
    }
}
