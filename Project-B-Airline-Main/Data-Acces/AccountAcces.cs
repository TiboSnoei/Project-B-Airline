using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

public class AccountAccess
{
    private readonly string _connectionString;

    public AccountAccess()
    {
        var dbPath = Path.Combine("data", "airline.db");
        _connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = dbPath,
            Mode = SqliteOpenMode.ReadWrite
        }.ToString();
    }

    public void Write(AccountModel account)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var sql = @"INSERT INTO Users (UserType, FirstName, LastName, Password, Email, created_at, TelNum, LoyaltyPoints)
                    VALUES (@UserType, @FirstName, @LastName, @Password, @Email, @CreatedAt, @TelNum, @LoyaltyPoints)";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@UserType", "Customer");
        command.Parameters.AddWithValue("@FirstName", account.FirstName);
        command.Parameters.AddWithValue("@LastName", account.LastName);
        command.Parameters.AddWithValue("@Password", account.Password);
        command.Parameters.AddWithValue("@Email", account.Email);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        command.Parameters.AddWithValue("@TelNum", account.TelNum);
        command.Parameters.AddWithValue("@LoyaltyPoints", 0);

        command.ExecuteNonQuery();
    }

    public AccountModel GetByEmail(string email)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var sql = "SELECT UserID, UserType, FirstName, LastName, Password, Email, created_at, TelNum, LoyaltyPoints FROM Users WHERE Email = @Email";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", email);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new AccountModel
            {
                UserID = reader.GetInt32(0),
                UserType = reader.GetString(1),
                FirstName = reader.GetString(2),
                LastName = reader.GetString(3),
                Password = reader.GetString(4),
                Email = reader.GetString(5),
                CreatedAt = DateTime.Parse(reader.GetString(6)),
                TelNum = reader.GetString(7),
                LoyaltyPoints = reader.GetInt32(8)
            };
        }

        return null;
    }

    public int GetIdByEmail(string email)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var sql = "SELECT UserID FROM Users WHERE Email = @Email";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", email);

        var result = command.ExecuteScalar();
        if (result != null)
        {
            return Convert.ToInt32(result);
        }

        return 0;
    }

    public List<AccountModel> GetAll()
    {
        var accounts = new List<AccountModel>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var sql = "SELECT UserID, UserType, FirstName, LastName, Password, Email, created_at, TelNum, LoyaltyPoints FROM Users";

        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            accounts.Add(new AccountModel
            {
                UserID = reader.GetInt32(0),
                UserType = reader.GetString(1),
                FirstName = reader.GetString(2),
                LastName = reader.GetString(3),
                Password = reader.GetString(4),
                Email = reader.GetString(5),
                CreatedAt = DateTime.Parse(reader.GetString(6)),
                TelNum = reader.GetString(7),
                LoyaltyPoints = reader.GetInt32(8)
            });
        }

        return accounts;
    }

    public void Update(AccountModel updatedAccount)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var sql = @"UPDATE Users
                    SET Email = @Email, Password = @Password, UserType = @UserType FirstName = @FirstName,
                        LastName = @LastName, TelNum = @TelNum, LoyaltyPoints = @LoyaltyPoints
                    WHERE UserID = @UserID";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", updatedAccount.Email);
        command.Parameters.AddWithValue("@Password", updatedAccount.Password);
        command.Parameters.AddWithValue("@UserType", updatedAccount.UserType);
        command.Parameters.AddWithValue("@FirstName", updatedAccount.FirstName);
        command.Parameters.AddWithValue("@LastName", updatedAccount.LastName);
        command.Parameters.AddWithValue("@TelNum", updatedAccount.TelNum);
        command.Parameters.AddWithValue("@LoyaltyPoints", updatedAccount.LoyaltyPoints);
        command.Parameters.AddWithValue("@UserID", updatedAccount.UserID);

        command.ExecuteNonQuery();
    }

    public void Delete(int userId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var sql = "DELETE FROM Users WHERE UserID = @UserID";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@UserID", userId);

        command.ExecuteNonQuery();
    }
}
